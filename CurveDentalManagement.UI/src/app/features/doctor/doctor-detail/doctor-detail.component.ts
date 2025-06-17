import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Doctor } from '../models/doctor.models';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { DoctorService } from '../services/doctor.service';
import { Appointment } from '../../appointments/models/appointment.models';
import { AppointmentService } from '../../appointments/services/appointment.service';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { Treatment } from '../../treatments/models/treatment.models';
import { TreatmentService } from '../../treatments/services/treatment.service';

@Component({
  selector: 'app-doctor-detail',
  imports: [RouterModule, CommonModule, NgbAlertModule],
  templateUrl: './doctor-detail.component.html',
  styleUrl: './doctor-detail.component.css'
})
export class DoctorDetailComponent implements OnInit {

  // add properties for doctorId
   DoctorsId!: string;
   AppointmentsId!: string;
   TreatmentsId!: string;

   doctors$?: Observable<Doctor>;
   appointments$?: Observable<Appointment[]>; 
   treatments$?: Observable<Treatment[]>

  // add constructor
  constructor(
    private doctorService: DoctorService,
    private appointmentService: AppointmentService, 
    private treatmentService: TreatmentService,
    private route: ActivatedRoute
  ) { }

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

     // Get the doctor ID from the route parameters
    this.DoctorsId = this.route.snapshot.paramMap.get('id') || '';
    this.AppointmentsId = this.route.snapshot.paramMap.get('appointmentId') || '';
    this.TreatmentsId = this.route.snapshot.paramMap.get('treatmentId') || '';

    // Fetch doctor details (return a single Doctor object)
    this.doctors$ = this.doctorService.getDoctorById(this.DoctorsId);

    // Fetch Appointments filtered by the Doctor ID
    this.appointments$ = this.appointmentService.getAllAppointments().pipe(
          map(appointments => 
            appointments.filter(appointment => 
              appointment.doctors.some(doctor => doctor.id === this.DoctorsId)
          )),
    );

    // Fetch Treatments filtered by the Doctor ID
    this.treatments$ = this.treatmentService.getAllTreatments().pipe(
          map(treatments => 
            treatments.filter(treatment => 
              treatment.doctors.some(doctor => doctor.id === this.DoctorsId)
          )),
    );
  }

}
