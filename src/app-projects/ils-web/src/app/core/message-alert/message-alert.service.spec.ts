import {TestBed} from '@angular/core/testing';

import {MessageAlertService} from './message-alert.service';
import {ApolloError} from "@apollo/client/errors";

describe('MessageAlertService', () => {
    let service: MessageAlertService;
    let swalSpy: jest.SpyInstance;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(MessageAlertService);
        swalSpy = jest.spyOn(service['themedSwal'], 'fire');
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('showPopup should call themesSwal.fire with default value', () => {
        swalSpy.mockReset();
        service.showPopup('success', 'Test message');
        expect(swalSpy).toHaveBeenCalledTimes(1);
    });

    it('showPopup should call themesSwal.fire with parameter value', () => {
        swalSpy.mockReset();
        service.showPopup('error', 'Test message');
        expect(swalSpy).toHaveBeenCalledWith({
            icon: 'error',
            title: 'Error',
            text: 'Test message',
        });

        swalSpy.mockReset();
        service.showPopup('info', 'Test message');
        expect(swalSpy).toHaveBeenCalledWith({
            icon: 'info',
            title: 'Information',
            text: 'Test message',
        });

        swalSpy.mockReset();
        service.showPopup('question', 'Test message');
        expect(swalSpy).toHaveBeenCalledWith({
            icon: 'question',
            title: 'Question',
            text: 'Test message',
        });

        swalSpy.mockReset();
        service.showPopup('success', 'Test message');
        expect(swalSpy).toHaveBeenCalledWith({
            icon: 'success',
            title: 'Success',
            text: 'Test message',
        });

        swalSpy.mockReset();
        service.showPopup('warning', 'Test message');
        expect(swalSpy).toHaveBeenCalledWith({
            icon: 'warning',
            title: 'Warning',
            text: 'Test message',
        });
    });
    
    it('should call showPopup with error when error type is ApolloError', () => {
        const error = new ApolloError({
            graphQLErrors: [{message: 'GraphQL error occurred'}],
            networkError: new Error('Network error occurred'),
        });

        swalSpy.mockReset();
        
        service.showHttpErrorPopup(error);
        
        expect(swalSpy).toHaveBeenCalledWith({
            icon: 'error',
            title: 'Error',
            text: 'Network error occurred',
        });
    });
});
