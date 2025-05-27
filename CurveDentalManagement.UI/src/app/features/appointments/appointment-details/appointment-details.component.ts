import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Appointment } from '../models/appointment.models';
import { AppointmentService } from '../services/appointment.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Doctor } from '../../doctor/models/doctor.models';
import { Patient } from '../../patient/models/patient.models';

@Component({
  selector: 'app-appointment-details',
  imports: [RouterModule, CommonModule],
  templateUrl: './appointment-details.component.html',
  styleUrl: './appointment-details.component.css'
})
export class AppointmentDetailsComponent implements OnInit {

  // add properties 
  AppointmentsId!: string;
  appointments$? : Observable<Appointment>;
  doctors$?: Observable<Doctor[]>;
  patients$?: Observable<Patient[]>;

  // constructor
   constructor(
    private appointmentService: AppointmentService,
    private route: ActivatedRoute
  ) {}

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    // get appointment Id from the route
    this.AppointmentsId = this.route.snapshot.paramMap.get('id') || '';

    // fetch appointment details & return single object
    this.appointments$ = this.appointmentService.getAppointmentById(this.AppointmentsId); 

    // fetch doctor filtered by the prescription ID
    this.doctors$ = this.appointments$.pipe(
      map(appointment => appointment?.doctors || []),
      catchError(error => {
        console.error('Error fetching appointment:', error);
        return of([]);
      })
    );

    // fetch patient filtered by the prescription ID
    this.patients$ = this.appointments$.pipe(
      map(appointment => appointment?.patients || []),
      catchError(error => {
        console.error('Error fetching appointment:', error);
        return of([]);
      })
    );


  }

}
