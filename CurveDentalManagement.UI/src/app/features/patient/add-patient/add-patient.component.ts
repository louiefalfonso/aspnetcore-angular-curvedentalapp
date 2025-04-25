import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgbAlertModule, NgbDatepickerModule, NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { AddPatientRequest } from '../models/add-patient-request.models';
import { Subscription } from 'rxjs';
import { PatientService } from '../services/patient.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-patient',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule, NgbDatepickerModule, NgbAlertModule],
  templateUrl: './add-patient.component.html',
  styleUrl: './add-patient.component.css'
})
export class AddPatientComponent implements OnDestroy {

  // add model
  model: AddPatientRequest;

  // add unsubcribe from observables
  private addPatientSubscription ?: Subscription;

  // Add toast visibility property
  showToast: boolean = false;

  // add constructor
  constructor(
    private patientService: PatientService,
    private http: HttpClient,
    private router: Router)
    {
      this.model = {
        firstName: '',
        lastName: '',
        dateOfBirth:  new Date(),
        gender: '',
        email: '',
        age: '',
        phoneNumber: '',
        address: '',
        insuranceDetails: '',
        insuranceProvider: '',
        insurancePolicyNumber: '',
        insuranceExpiryDate: new Date(),
      }
  }

  // add onFormSubmit
  onFormSubmit() {
      this.addPatientSubscription = this.patientService.addNewPatient(this.model)
        .subscribe({
          next: (response) => {
            this.showToast = true; 
            setTimeout(() => {
              this.showToast = false;
              this.router.navigate(['/admin/patients']);
            }, 2000);
          },
          error: (error) => {
            console.error(error);
          }
        });
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
   this.addPatientSubscription?.unsubscribe();
  }

}
