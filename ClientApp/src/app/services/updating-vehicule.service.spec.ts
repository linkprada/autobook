import { TestBed } from '@angular/core/testing';

import { UpdatingVehiculeService } from './updating-vehicule.service';

describe('UpdatingVehiculeService', () => {
  let service: UpdatingVehiculeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdatingVehiculeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
