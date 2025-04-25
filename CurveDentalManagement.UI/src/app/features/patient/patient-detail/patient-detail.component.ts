import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PatientService } from '../services/patient.service';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.models';

@Component({
  selector: 'app-patient-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './patient-detail.component.html',
  styleUrl: './patient-detail.component.css'
})
export class PatientDetailComponent implements OnInit {

   // add properties for patientId
   PatientsId!: string; 


   // add properties for patients
   patients$?: Observable<Patient>;

  // add constructor
  constructor(
    private patientService: PatientService,
    private route: ActivatedRoute
  ) { }


   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

    // Get the patient ID from the route parameters
    this.PatientsId = this.route.snapshot.paramMap.get('id') || '';


    // Fetch patient details (return a single Patient object)
    this.patients$ = this.patientService.getPatientById(this.PatientsId);
  }

}
