import { TestBed } from '@angular/core/testing';

import { MessageAlertService } from './message-alert.service';

describe('MessageAlertService', () => {
  let service: MessageAlertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MessageAlertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
