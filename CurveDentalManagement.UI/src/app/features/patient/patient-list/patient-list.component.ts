import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbAlertModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { Observable,  of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';


import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';
import { PatientChartComponent } from '../patient-chart/patient-chart.component';

@Component({
  selector: 'app-patient-list',
  imports: [RouterModule, CommonModule, NgbPaginationModule, NgbAlertModule, PatientChartComponent],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.css'
})
export class PatientListComponent implements OnInit{

  // Define the observable for patients
  patients$?: Observable<Patient[]>;
  allPatients: Patient[] = [];

  // For sorting, filtering & pagination
  totalCount: number = 0;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

 // Add constructor
 constructor(
  private patientService: PatientService,
  private route: ActivatedRoute
  ) { }

  // Implement ngOnInit lifecycle hook
  ngOnInit(): void {
  
  // Load all patients for the chart
  this.loadAllPatients();

  this.patientService.getPatientCount()
   .subscribe({
    next: (value) => {
      this.totalCount = value;
       this.list = new Array(Math.ceil(value / this.pageSize))

       this.patients$ = this.patientService.getAllPatients(
         undefined,
         undefined,
         undefined,
         this.pageNumber,
         this.pageSize
       );
    } 
   })
  }

    // Implement loadAllStaffs method for chart display
    loadAllPatients(): void {
      this.patientService.getAllPatients().pipe(
        map((patients: Patient[] | null) => patients ?? []), 
        catchError(() => of([])) 
      ).subscribe((patients) => {
        this.allPatients = patients; 
      });
    }

  // implement search
  onSearch(query: string) {
    this.patients$ = this.patientService.getAllPatients(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.patients$ = this.patientService.getAllPatients(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.patients$ = this.patientService.getAllPatients(undefined, sortBy, sortDirection);
  }

  // Implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.patients$ = this.patientService.getAllPatients(
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
    this.patients$ = this.patientService.getAllPatients(
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
    this.patients$ = this.patientService.getAllPatients(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

}
