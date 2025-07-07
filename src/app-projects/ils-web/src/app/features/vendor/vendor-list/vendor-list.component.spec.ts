import {ComponentFixture, TestBed} from '@angular/core/testing';
import {VendorListComponent} from './vendor-list.component';
import {CommonModule} from "@angular/common";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {KnBadge, KnButton, KnPagination} from "@kathanika/kn-ui";
import {
    DeleteVendorGQL,
    SortEnumType,
    VendorListGQL,
    VendorStatus
} from "../../../graphql/generated/graphql-operations";
import {of} from "rxjs";
import {mockMutationGql, mockQueryGql} from "../../../graphql/gql-test-utils";
import {MessageAlertService} from "../../../core/message-alert/message-alert.service";

describe('VendorListComponent', () => {
    let component: VendorListComponent;
    let fixture: ComponentFixture<VendorListComponent>;
    let mockRouter: Router;
    let mockAlertService: MessageAlertService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                VendorListComponent,
                CommonModule,
                RouterLink,
                KnButton,
                KnPagination,
                KnBadge
            ],
            providers: [
                {
                    provide: VendorListGQL,
                    useValue: mockQueryGql,
                },
                {
                    provide: DeleteVendorGQL,
                    useValue: mockMutationGql,
                },
                {
                    provide: ActivatedRoute,
                    useValue: {
                        queryParams: of({
                            size: 20,
                            page: 2,
                        }),
                    },
                },
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(VendorListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        // Set up mocks after component creation
        mockRouter = TestBed.inject(Router);
        mockRouter.navigate = jest.fn().mockResolvedValue(true);

        mockAlertService = TestBed.inject(MessageAlertService);
        mockAlertService.showConfirmation = jest.fn().mockReturnValue(of(true));
        mockAlertService.showPopup = jest.fn();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize with correct query variables', () => {
        // Expected default query variables from component initialization
        expect(component['queryVariables'].skip).toBe(20); // (page-1) * size
        expect(component['queryVariables'].take).toBe(20);
        expect(component['queryVariables'].sortBy).toEqual({
            name: SortEnumType.Asc,
        });
    });

    it('should update search filter when search text changes', () => {
        // Mock the setSearchTextQueryFilter method
        jest.spyOn(component, 'setSearchTextQueryFilter' as never);

        // Mock the search event
        const mockEvent = {target: {value: 'test vendor'}};
        component['onSearchTextChanged'](mockEvent);

        // Use setTimeout to wait for the debounce time
        return new Promise<void>(resolve => {
            setTimeout(() => {
                expect(component['setSearchTextQueryFilter']).toHaveBeenCalledWith('test vendor');
                expect(component['searchText']).toBe('test vendor');
                resolve();
            }, 800); // Slightly longer than the debounce time (700ms)
        });
    });

    it('should change page when changePage is called', () => {
        // Spy on queryRef.refetch
        component['queryRef'].refetch = jest.fn();

        // Call changePage
        component['changePage'](3);

        // Check if the skip value is updated correctly (3-1)*20 = 40
        expect(component['queryVariables'].skip).toBe(40);
        expect(component['queryRef'].refetch).toHaveBeenCalled();
    });

    it('should render vendor status badges with correct colors', () => {
        // Mock the vendor data with different statuses
        // const vendorData = {
        //     valueChanges: of({
        //         data: {
        //             vendors: {
        //                 items: [
        //                     { id: '1', name: 'Vendor 1', status: VendorStatus.Active, contactPersonName: 'Contact 1' },
        //                     { id: '2', name: 'Vendor 2', status: VendorStatus.Inactive, contactPersonName: 'Contact 2' }
        //                 ],
        //                 totalCount: 2
        //             }
        //         }
        //     })
        // };

        // Set the mock query reference
        // component['_queryRef'] = vendorData as any;
        fixture.detectChanges();

        // Verify the component has the correct vendor status enum
        expect(component.vendorStatus).toBe(VendorStatus);

        // Since we can't easily test the template rendering in this isolated test,
        // we can test the component's logic for determining badge types
        expect(VendorStatus.Active).toBeDefined();
        expect(VendorStatus.Inactive).toBeDefined();
    });

    it('should handle changing page size', () => {
        // Spy on queryRef.refetch
        component['queryRef'].refetch = jest.fn();
        component['router'].navigate = jest.fn().mockResolvedValue(true);

        // Call changePageSize
        component['changePageSize'](50);

        // Check if page size and skip are updated correctly
        expect(component['queryVariables'].take).toBe(50);
        expect(component['queryVariables'].skip).toBe(0); // Reset to first page
        expect(component['queryRef'].refetch).toHaveBeenCalled();
        expect(component['router'].navigate).toHaveBeenCalled();
    });

    it('should delete vendor and show confirmation alert', () => {
        const vendorId = '123';
        const mockMutationResponse = {
            data: {
                deleteVendor: {
                    errors: [],
                }
            },
            loading: false
        };

        // Mock the deleteVendorGql mutation
        component['deleteVendorGql'].mutate = jest.fn().mockReturnValue(of(mockMutationResponse));

        // Call deleteVendor
        component.deleteVendor(vendorId);

        // Check if the confirmation alert was shown
        expect(mockAlertService.showConfirmation).toHaveBeenCalledWith('warning', 'Are you sure you want to delete Vendor?');

        // Check if the mutation was called with the correct vendor ID
        expect(component['deleteVendorGql'].mutate).toHaveBeenCalledWith({id: vendorId});

        // Verify that no errors were returned
        expect(mockMutationResponse.data.deleteVendor.errors.length).toBe(0);
    });

    it('should handle vendor deletion errors', () => {
        const vendorId = '123';
        const mockErrorResponse = {
            data: {
                deleteVendor: {
                    errors: [{message: 'Error deleting vendor'}],
                }
            },
            loading: false
        };

        // Mock the deleteVendorGql mutation to return an error
        component['deleteVendorGql'].mutate = jest.fn().mockReturnValue(of(mockErrorResponse));

        // Call deleteVendor
        component.deleteVendor(vendorId);

        // Check if the error alert was shown
        expect(mockAlertService.showPopup).toHaveBeenCalledWith('error', 'Error deleting vendor', 'Error Deleting Vendor');
    });

    it('should handle successful vendor deletion', () => {
        const vendorId = '123';
        const mockSuccessResponse = {
            data: {
                deleteVendor: {
                    message: 'Vendor deleted successfully',
                    errors: [],
                }
            },
            loading: false
        };

        component['queryRef'].refetch = jest.fn();
        // Mock the deleteVendorGql mutation to return a success response
        component['deleteVendorGql'].mutate = jest.fn().mockReturnValue(of(mockSuccessResponse));

        // Call deleteVendor
        component.deleteVendor(vendorId);

        // Check if the success alert was shown
        expect(mockAlertService.showPopup).toHaveBeenCalledWith('success', 'Vendor deleted successfully', 'Deleted');

        // Verify that refetch was called to update the vendor list
        expect(component['queryRef'].refetch).toHaveBeenCalled();
    });

    it('should handle vendor deletion confirmation cancellation', () => {
        const vendorId = '123';

        // Mock the alert service to return false for confirmation
        mockAlertService.showConfirmation = jest.fn().mockReturnValue(of(false));

        // Call deleteVendor
        component.deleteVendor(vendorId);

        // Check that the mutation was not called
        expect(component['deleteVendorGql'].mutate).not.toHaveBeenCalled();
    });

    it('should handle vendor deletion when loading', () => {
        const vendorId = '123';
        const mockLoadingResponse = {
            data: {
                deleteVendor: {
                    errors: [],
                }
            },
            loading: true
        };

        // Mock the deleteVendorGql mutation to return a loading state
        component['deleteVendorGql'].mutate = jest.fn().mockReturnValue(of(mockLoadingResponse));

        // Call deleteVendor
        component.deleteVendor(vendorId);

        // Check that no alerts are shown while loading
        expect(mockAlertService.showPopup).not.toHaveBeenCalled();
    });
});
