import {ComponentFixture, TestBed} from '@angular/core/testing';
import {BookRecordFormComponent} from './book-record-form.component';
import {ReactiveFormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {KnTextInput, KnButton} from '@kathanika/kn-ui';
import {ApolloTestingModule} from 'apollo-angular/testing';
import {CreateBibRecordGQL} from '../../../graphql/generated/graphql-operations';

describe('BookRecordFormComponent', () => {
    let fixture: ComponentFixture<BookRecordFormComponent>;
    let component: BookRecordFormComponent;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                CommonModule,
                ReactiveFormsModule,
                ApolloTestingModule,
                BookRecordFormComponent,
                KnTextInput,
                KnButton
            ],
            providers: [
                {
                    provide: CreateBibRecordGQL,
                    useValue: {
                        mutate: jest.fn()
                    }
                }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(BookRecordFormComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});