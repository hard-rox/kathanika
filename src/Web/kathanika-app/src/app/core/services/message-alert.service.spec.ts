import { TestBed } from '@angular/core/testing';

import { MessageAlertService } from './message-alert.service';

describe('MessageAlertService', () => {
  let service: MessageAlertService;
  let swalSpy: jasmine.Spy<any>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MessageAlertService);
    swalSpy = spyOn<any>(service['themedSwal'], 'fire');
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('showSuccess should call themesSwal.fire with default value', () => {
    service.showPopup('success', 'Test message');
    expect(swalSpy).toHaveBeenCalledTimes(1);
  });
});
