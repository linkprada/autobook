import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewVehiculeComponent } from './view-vehicule.component';

describe('ViewVehiculeComponent', () => {
  let component: ViewVehiculeComponent;
  let fixture: ComponentFixture<ViewVehiculeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewVehiculeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewVehiculeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
