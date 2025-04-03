import { TestBed } from '@angular/core/testing';

import { LogisticianService } from './logistician.service';

describe('UserService', () => {
  let service: LogisticianService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LogisticianService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
