import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnPagination } from './pagination.component';

describe('KnPagination', () => {
  let component: KnPagination;
  let fixture: ComponentFixture<KnPagination>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KnPagination],
    }).compileComponents();
    fixture = TestBed.createComponent(KnPagination);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set last page on setting totalCount', () => {
    component.totalCount = 10;
    fixture.detectChanges();

    expect(component['_totalCount']).toEqual(10);
    expect(component.lastPage).toEqual(10);
  });

  it('should set last page on setting pageSizes', () => {
    component.totalCount = 10;
    component.pageSizes = [3, 4, 5];
    fixture.detectChanges();

    expect(component.lastPage).toEqual(4);
  });

  it('should show default page sizes when not setting pageSizes input', () => {
    fixture.detectChanges();
    const nativeElement = fixture.nativeElement as HTMLElement;
    const pageSizeOptionNodes = nativeElement.querySelectorAll('option');
    const pageSizeOptionValues = Array.from(pageSizeOptionNodes).map(
      (x) => +x.value,
    );

    expect(component.pageSizes).toEqual([5, 10, 20, 50, 100]);
    expect(pageSizeOptionNodes.length).toEqual(5);
    expect(pageSizeOptionValues).toEqual([5, 10, 20, 50, 100]);
  });

  it('should set last page on setting pageSize', () => {
    component.totalCount = 10;
    component.pageSize = 10;
    fixture.detectChanges();

    expect(component.lastPage).toEqual(1);
  });

  it('should emit pageSizeChanged on pageSize change', () => {
    const pageSizeChangedOutputSpy = jest.spyOn(
      component['pageSizeChanged'],
      'emit',
    );
    component.pageSizes = [1, 2, 3];
    fixture.detectChanges();
    const selectElement = fixture.nativeElement.querySelector(
      'select',
    ) as HTMLSelectElement;
    selectElement.selectedIndex = 1;
    selectElement.dispatchEvent(new Event('change'));
    fixture.detectChanges();

    expect(pageSizeChangedOutputSpy).toHaveBeenCalled();
  });

  it('should emit pageChanged on page change', () => {
    const pageChangedOutputSpy = jest.spyOn(component['pageChanged'], 'emit');
    component['onPageChanged'](1);
    fixture.detectChanges();

    expect(pageChangedOutputSpy).toHaveBeenCalledWith(1);
  });

  it('should not emit pageChanged on page change when pageNumber is greater than lastPage', () => {
    const pageChangedOutputSpy = jest.spyOn(component['pageChanged'], 'emit');
    component['onPageChanged'](3);
    fixture.detectChanges();

    expect(pageChangedOutputSpy).not.toHaveBeenCalled();
  });
});
