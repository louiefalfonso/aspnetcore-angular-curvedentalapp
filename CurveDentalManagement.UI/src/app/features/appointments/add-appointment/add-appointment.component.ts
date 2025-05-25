import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgbToastModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { AddAppointmentRequest } from '../models/add-appointment-request.models';
import { Patient } from '../../patient/models/patient.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { AppointmentService } from '../services/appointment.service';
import { HttpClient } from '@angular/common/http';
import { DoctorService } from '../../doctor/services/doctor.service';
import { PatientService } from '../../patient/services/patient.service';

@Component({
  selector: 'app-add-appointment',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule, NgbAlertModule],
  templateUrl: './add-appointment.component.html',
  styleUrl: './add-appointment.component.css'
})
export class AddAppointmentComponent implements OnInit, OnDestroy{

  // add model
  model: AddAppointmentRequest;
  doctors$? : Observable<Doctor[]>;
  patients$? : Observable<Patient[]>;

  // add unsubcribe from observables
  private addAppointmentSubscription ?: Subscription;

  // add toast visibility property
  showToast: boolean = false;

  // add constructor
  constructor(
    private appointmentService: AppointmentService,
    private doctorService: DoctorService,
    private patientService: PatientService,
    private http: HttpClient,
    private router: Router
  ){
    this.model = {
    status: "",
    remarks: "",
    appointmentCode: "",
    appointmentDate: new Date(),
    appointmentTime: "",
    doctors:[],
    patients:[],
    }
  }
  

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    // display all patients
     this.patients$ = this.patientService.getAllPatients();

    // display all doctors
    this.doctors$ = this.doctorService.getAllDoctors();
  }

  // add onFormSubmit
  onFormSubmit() {
   this.addAppointmentSubscription = this.appointmentService.addNewAppointment(this.model)
    .subscribe({
       next: (response) => {
        this.showToast = true; 
        setTimeout(() => {
          this.showToast = false;
          this.router.navigate(['/admin/appointments']);
        }, 2000);
      },
      error: (error) => {
        console.error(error);
      }
    })
  }


  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addAppointmentSubscription?.unsubscribe();
  }

}
