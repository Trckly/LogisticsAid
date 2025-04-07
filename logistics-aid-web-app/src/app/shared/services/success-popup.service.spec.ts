import { TestBed } from '@angular/core/testing';

import { SuccessPopupService } from './success-popup.service';

describe('SuccessPopupService', () => {
  let service: SuccessPopupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SuccessPopupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
