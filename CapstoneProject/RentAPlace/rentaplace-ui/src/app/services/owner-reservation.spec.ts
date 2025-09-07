import { TestBed } from '@angular/core/testing';

import { OwnerReservation } from './owner-reservation';

describe('OwnerReservation', () => {
  let service: OwnerReservation;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OwnerReservation);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
