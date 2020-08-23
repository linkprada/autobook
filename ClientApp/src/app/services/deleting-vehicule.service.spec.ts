import { TestBed } from '@angular/core/testing';

import { DeletingVehiculeService} from './deleting-vehicule.service';

describe('DeletingVehiculeService', () => {
  let service: DeletingVehiculeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeletingVehiculeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
