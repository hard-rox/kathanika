import {ComponentFixture, TestBed} from '@angular/core/testing';
import {BookRecordFormComponent} from './book-record-form.component';
import {ReactiveFormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {KnTextInput, KnButton} from '@kathanika/kn-ui';
import {ApolloTestingModule} from 'apollo-angular/testing';
import {CreateBibRecordGQL, CreateBibRecordInput} from '../../../graphql/generated/graphql-operations';
import {Component} from '@angular/core';

describe('BookRecordFormComponent', () => {
    let hostComponent: BookRecordFormComponent;
    let hostFixture: ComponentFixture<BookRecordFormComponent>;
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

        hostFixture = TestBed.createComponent(BookRecordFormComponent);
        hostComponent = hostFixture.componentInstance;
        component = hostFixture.debugElement.children[0].componentInstance;
        hostFixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});