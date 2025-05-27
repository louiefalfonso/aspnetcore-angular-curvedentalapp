import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgbToastModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { Patient } from '../../patient/models/patient.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { Appointment } from '../models/appointment.models';
import { AppointmentService } from '../services/appointment.service';
import { DoctorService } from '../../doctor/services/doctor.service';
import { PatientService } from '../../patient/services/patient.service';
import { UpdateAppointmentRequest } from '../models/update-appointment-request.models';

@Component({
  selector: 'app-edit-appointment',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule, NgbAlertModule],
  templateUrl: './edit-appointment.component.html',
  styleUrl: './edit-appointment.component.css'
})
export class EditAppointmentComponent implements OnInit, OnDestroy {

  // add id property
  id : string | null = null;
  model?: Appointment;
  doctors$? : Observable<Doctor[]>
  patients$? : Observable<Patient[]>
  selectedDoctor?: string[];
  selectedPatient?: string[];

  // add Subscription
  routeSubscription?: Subscription;
  getAppointmentSubscription?: Subscription;
  editAppointmentSubscription?: Subscription;
  deleteAppointmentSubscription? : Subscription;

  // add constructor and inject the necessary services
  constructor(
      private appointmentService: AppointmentService,
      private doctorService: DoctorService,
      private patientService: PatientService,
      private router: Router,
      private route : ActivatedRoute,
  ){}

  // Add toast visibility property
  showToast: boolean = false;
  toastMessage: string = '';

  // add appointment object
  appointment?:Appointment;


  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    //get all doctors
    this.doctors$ = this.doctorService.getAllDoctors();

    //get all patients
    this.patients$ = this.patientService.getAllPatients();

      // get all id of the appointment to edit
   this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) =>{
        this.id = params.get('id');

        if(this.id){
         this.getAppointmentSubscription = this.appointmentService.getAppointmentById(this.id).subscribe({
            next: (response) =>{
              this.model = response;
              this.selectedDoctor = response.doctors.map(x => x.id);
              this.selectedPatient = response.patients.map(x => x.id);
            },
            error: (error) => {
              console.error(error);
            }
          })
        }
      }
    })
  }

  // implement onDelete
  onDelete():void{
      if(this.id){
      this.deleteAppointmentSubscription= this.appointmentService.deleteAppointment(this.id).subscribe({
          next: (response) => {
            this.toastMessage = 'Appointment Deleted Successfully!';
            this.showToast = true;
            setTimeout(() => {
              this.showToast = false; 
              this.router.navigate(['/admin/appointments']);
            }, 2000);
          },
          error: (error) => {
            console.error(error);
          }
        });
      }
  }

  // implement onSubmit 
  onFormSubmit(): void {
    // convert this method to request object
    if(this.model && this.id){
      var updateAppointmentRequest: UpdateAppointmentRequest ={
        status: this.model.status?? "",
        remarks: this.model.remarks?? "",
        appointmentDate: typeof this.model?. appointmentDate === 'string' ? new Date(this.model.appointmentDate) : this.model?.appointmentDate?? new Date(),
        appointmentTime: this.model.appointmentTime?? "",
        appointmentCode: this.model.appointmentCode?? "",
        doctors:this.selectedDoctor??[],
        patients:this.selectedPatient??[]
      };

      // pass this object to the service
        if(this.id){
          this. editAppointmentSubscription = this.appointmentService.updateAppointment(this.id, updateAppointmentRequest)
          .subscribe({
             next: (response) => {
              this.showToast = true; 
              this.toastMessage = 'Appointment Updated Successfully!';
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
    }
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.getAppointmentSubscription?.unsubscribe();
    this.editAppointmentSubscription?.unsubscribe();
    this.deleteAppointmentSubscription?.unsubscribe();
  }

}
