import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgbToastModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { AddTreatmentRequest } from '../models/add-treatment-request.models';
import { Observable, Subscription } from 'rxjs';
import { TreatmentService } from '../services/treatment.service';
import { HttpClient } from '@angular/common/http';
import { Doctor } from '../../doctor/models/doctor.models';
import { DoctorService } from '../../doctor/services/doctor.service';

@Component({
  selector: 'app-add-treatment',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule, NgbAlertModule],
  templateUrl: './add-treatment.component.html',
  styleUrl: './add-treatment.component.css'
})
export class AddTreatmentComponent implements OnInit, OnDestroy {

  // add model
  model: AddTreatmentRequest;
  doctors$? : Observable<Doctor[]>;
  
  // add unsubcribe from observables
  private addTreatmentSubscription ?: Subscription;

  // Add toast visibility property
  showToast: boolean = false;

// add constructor
  constructor(
    private treatmentService: TreatmentService,
    private doctorService: DoctorService,
    private http: HttpClient,
    private router: Router
  ){
    this.model = {
      treatmentName: "",
      treatmentCode: "",
      description: "",
      duration: "",
      cost: "",
      insuranceCoverage: "",
      insuranceCoverageAmount: "",
      followUpCare: "",
      riskBenefits: "",
      indications: "",
      doctors:[],
    }
  }

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

     // display all doctors
     this.doctors$ = this.doctorService.getAllDoctors();
  }

  // add onFormSubmit
   onFormSubmit() {
   this.addTreatmentSubscription = this.treatmentService.addNewTreatment(this.model)
    .subscribe({
       next: (response) => {
        this.showToast = true; 
        setTimeout(() => {
          this.showToast = false;
          this.router.navigate(['/admin/treatments']);
        }, 2000);
      },
      error: (error) => {
        console.error(error);
      }
    })
   }

   // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addTreatmentSubscription?.unsubscribe();
  }
}
