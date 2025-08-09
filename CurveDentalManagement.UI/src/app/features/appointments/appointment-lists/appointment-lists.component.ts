import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { Appointment } from '../models/appointment.models';
import { Observable } from 'rxjs';
import { AppointmentService } from '../services/appointment.service';

@Component({
  selector: 'app-appointment-lists',
  imports: [RouterModule, CommonModule, NgbPaginationModule, NgbAlertModule],
  templateUrl: './appointment-lists.component.html',
  styleUrl: './appointment-lists.component.css'
})
export class AppointmentListsComponent implements OnInit {

  // Define the observable for appointments
  appointments$? : Observable<Appointment[]>;

  // For sorting, filtering & pagination
  totalCount: number = 0;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

// Add constructor
 constructor(
  private appointmentService: AppointmentService,
  private route: ActivatedRoute
  ) { }


  // Implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.appointmentService.getAppointmentCount().subscribe({
      next:(value) =>{
        this.totalCount = value;
        this.list = new Array(Math.ceil(value / this.pageSize))
        
        this.appointments$ = this.appointmentService.getAllAppointments(
          undefined,
          undefined,
          undefined,
          this.pageNumber,
          this.pageSize
        );
      }
    })
  }

   // implement search
  onSearch(query: string) {
     this.appointments$ = this.appointmentService.getAllAppointments(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.appointments$ = this.appointmentService.getAllAppointments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
     this.appointments$ = this.appointmentService.getAllAppointments(undefined, sortBy, sortDirection);
  }

// Implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.appointments$ = this.appointmentService.getAllAppointments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

  // Implement getNextPage
  getNextPage() {
    if (this.pageNumber + 1 > this.list.length) {
      return;
    }
    this.pageNumber += 1;
    this.appointments$ = this.appointmentService.getAllAppointments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

  
  // implemennt getPrevPage
  getPrevPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
     this.appointments$ = this.appointmentService.getAllAppointments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }
}
