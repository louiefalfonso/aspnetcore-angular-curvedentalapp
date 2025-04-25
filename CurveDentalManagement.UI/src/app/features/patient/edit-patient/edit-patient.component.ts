import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';
import { UpdatePatientRequest } from '../models/update-patient-request.models';

@Component({
  selector: 'app-edit-patient',
  imports: [RouterModule, FormsModule, CommonModule, NgbToastModule],
  templateUrl: './edit-patient.component.html',
  styleUrl: './edit-patient.component.css'
})
export class EditPatientComponent implements OnInit, OnDestroy {

  // add id property
  id : string | null = null;

  // add Subscription
  routeSubscription?: Subscription;
  editPatientSubscription?: Subscription;
  deletePatientSubscription? : Subscription;

  // add patient object
  model?: Patient;

  // Add toast visibility property
  showToast: boolean = false;
  toastMessage: string = '';

  // add constructor and inject the necessary services
  constructor(
    private patientService: PatientService,
    private router: Router,
    private route : ActivatedRoute
  ) { } 
  

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
     // get the id of the patient to edit
     this.routeSubscription = this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if (this.id) {
        // get the patient details
        this.editPatientSubscription = this.patientService.getPatientById(this.id).subscribe({
          next: (response) => {
            this.model = response;
          },
          error: (error) => {
            console.error(error);
          }
        });
      }
    });
  }

  // implement onSubmit 
  onFormSubmit(): void {

    // convert this method to request object
    if(this.model && this.id){
      var updatePatientRequest : UpdatePatientRequest = {
        firstName: this.model.firstName?? '',
        lastName: this.model.lastName?? '',
        dateOfBirth: typeof this.model?. dateOfBirth === 'string' ? new Date(this.model.dateOfBirth) : this.model?.dateOfBirth?? new Date(),
        gender: this.model.gender?? '',
        email: this.model.email?? '',
        phoneNumber: this.model.phoneNumber?? '',
        age: this.model.age?? '',
        address: this.model.address?? '',
        insuranceDetails: this.model.insuranceDetails?? '',
        insuranceProvider: this.model.insuranceProvider?? '',
        insurancePolicyNumber:  this.model.insurancePolicyNumber?? '',
        insuranceExpiryDate: typeof this.model?. insuranceExpiryDate === 'string' ? new Date(this.model.insuranceExpiryDate) : this.model?.insuranceExpiryDate?? new Date(),
      }

      //pass this object to the service
      if(this.id){
        this.editPatientSubscription = this.patientService.updatePatient(this.id, updatePatientRequest)
        .subscribe({
          next: (response) => {
            this.showToast = true; 
            this.toastMessage = 'Patient Updated Successfully!';
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
    }
  }


  // implement onDelete
  onDelete():void{
      if(this.id){
      this.deletePatientSubscription = this.patientService.deletePatient(this.id).subscribe({
          next: (response) => {
            this.toastMessage = 'Patient Deleted Successfully!';
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
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.editPatientSubscription?.unsubscribe();
    this.deletePatientSubscription?.unsubscribe();
  }

}
