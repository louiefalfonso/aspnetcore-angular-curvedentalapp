import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorChartComponent } from './doctor-chart.component';

describe('DoctorChartComponent', () => {
  let component: DoctorChartComponent;
  let fixture: ComponentFixture<DoctorChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
