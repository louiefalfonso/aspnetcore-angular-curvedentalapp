import { TestBed } from '@angular/core/testing';

import { StaffService } from '../services/staff.service';

describe('StaffService', () => {
  let service: StaffService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaffService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
