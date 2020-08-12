import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingVehiculeFormComponent } from './adding-vehicule-form.component';

describe('AddingVehiculeFormComponent', () => {
  let component: AddingVehiculeFormComponent;
  let fixture: ComponentFixture<AddingVehiculeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddingVehiculeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddingVehiculeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
