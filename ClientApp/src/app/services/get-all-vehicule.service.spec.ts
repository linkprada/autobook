import { TestBed } from '@angular/core/testing';

import { GetAllVehiculeService } from './get-all-vehicule.service';

describe('GetAllVehiculeService', () => {
  let service: GetAllVehiculeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetAllVehiculeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
