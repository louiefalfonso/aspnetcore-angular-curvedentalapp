import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgbToastModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Treatment } from '../../treatments/models/treatment.models';
import { Patient } from '../../patient/models/patient.models';
import { BillingService } from '../services/billing.service';
import { PatientService } from '../../patient/services/patient.service';
import { TreatmentService } from '../../treatments/services/treatment.service';
import { AddBillingRequest } from '../models/add-billing-request.models';

@Component({
  selector: 'app-add-billing',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule, NgbAlertModule],
  templateUrl: './add-billing.component.html',
  styleUrl: './add-billing.component.css'
})
export class AddBillingComponent implements OnInit, OnDestroy{

  // add model
  model: AddBillingRequest;
  patients$? : Observable<Patient[]>;
  treatments$?: Observable<Treatment[]>;
  
  // add unsubcribe from observables
  private addBillingSubscription ?: Subscription;

  // add toast visibility property
  showToast: boolean = false;

  // add constructor
  constructor(
    private billingService : BillingService,
    private patientService: PatientService,
    private treatmentService: TreatmentService,
    private http: HttpClient,
    private router: Router
  ) { 
    this.model = {
      billingCode:"",
      billingStatus:"",
      billingDate:  new Date(),
      totalAmount: "",
      paymentStatus:"",
      paymentMethod: "",
      paymentDate:  new Date(),
      remarks: "",
      patients:[],
      treatments:[],
    }
  }

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    // displays all patients
    this.patients$ = this.patientService.getAllPatients();

    // displays alll treatments
    this.treatments$ = this.treatmentService.getAllTreatments();
  }

  // add onFormSubmit
  onFormSubmit() {
    this.addBillingSubscription = this.billingService.addNewBilling(this.model)
      .subscribe({
          next: (response) => {
            this.showToast = true; 
            setTimeout(() => {
              this.showToast = false;
              this.router.navigate(['/admin/billings']);
            }, 2000);
          },
          error: (error) => {
            console.error(error);
          }
    })
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addBillingSubscription?.unsubscribe();
  }

}
