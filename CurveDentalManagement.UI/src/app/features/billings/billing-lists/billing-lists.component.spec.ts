import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BillingListsComponent } from './billing-lists.component';

describe('BillingListsComponent', () => {
  let component: BillingListsComponent;
  let fixture: ComponentFixture<BillingListsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BillingListsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BillingListsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
