import { TestBed } from '@angular/core/testing';

import { MakeService } from './make.service';

describe('AddingVehiculeService', () => {
  let service: MakeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MakeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
