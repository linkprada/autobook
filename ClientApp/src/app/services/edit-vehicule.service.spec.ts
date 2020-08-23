import { TestBed } from '@angular/core/testing';

import { EditVehiculeService } from './edit-vehicule.service';

describe('EditVehiculeService', () => {
  let service: EditVehiculeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditVehiculeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
