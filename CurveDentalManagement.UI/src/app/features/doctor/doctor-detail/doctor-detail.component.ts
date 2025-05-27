import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Doctor } from '../models/doctor.models';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DoctorService } from '../services/doctor.service';
import { Appointment } from '../../appointments/models/appointment.models';
import { AppointmentService } from '../../appointments/services/appointment.service';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

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

   doctors$?: Observable<Doctor>;
   appointments$?: Observable<Appointment[]>; 

  // add constructor
  constructor(
    private doctorService: DoctorService,
    private appointmentService: AppointmentService, 
    private route: ActivatedRoute
  ) { }

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

     // Get the doctor ID from the route parameters
    this.DoctorsId = this.route.snapshot.paramMap.get('id') || '';
     this.AppointmentsId = this.route.snapshot.paramMap.get('appointmentId') || '';

    // Fetch doctor details (return a single Doctor object)
    this.doctors$ = this.doctorService.getDoctorById(this.DoctorsId);

    // Fetch Appointments filtered by the Doctor ID
    this.appointments$ = this.appointmentService.getAllAppointments().pipe(
          map(appointments => 
            appointments.filter(appointment => 
              appointment.doctors.some(doctor => doctor.id === this.DoctorsId)
          )),
    );
  }

}
