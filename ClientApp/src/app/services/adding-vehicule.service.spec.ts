import { TestBed } from '@angular/core/testing';

import { AddingVehiculeService } from './adding-vehicule.service';

describe('AddingVehiculeService', () => {
  let service: AddingVehiculeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddingVehiculeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
