import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PatientService } from '../services/patient.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Patient } from '../models/patient.models';
import { Appointment } from '../../appointments/models/appointment.models';
import { AppointmentService } from '../../appointments/services/appointment.service';

@Component({
  selector: 'app-patient-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './patient-detail.component.html',
  styleUrl: './patient-detail.component.css'
})
export class PatientDetailComponent implements OnInit {

   // add properties for patientId
   PatientsId!: string; 
   AppointmentsId!: string;
   patients$?: Observable<Patient>;
   appointments$?: Observable<Appointment[]>;

  // add constructor
  constructor(
    private patientService: PatientService,
    private appointmentService: AppointmentService, 
    private route: ActivatedRoute
  ) { }


   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

    // Get the patient ID from the route parameters
    this.PatientsId = this.route.snapshot.paramMap.get('id') || '';
    this.AppointmentsId = this.route.snapshot.paramMap.get('appointmentId') || '';

    // Fetch Patient details (return a single Patient object)
    this.patients$ = this.patientService.getPatientById(this.PatientsId);

    // Fetch Appointments filtered by the patient ID
    this.appointments$ = this.appointmentService.getAllAppointments().pipe(
      map(appointments => 
        appointments.filter(appointment => 
          appointment.patients.some(patient => patient.id === this.PatientsId)
      )),
    );

  }

}
