import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Doctor } from '../models/doctor.models';
import { Observable } from 'rxjs';
import { DoctorService } from '../services/doctor.service';

@Component({
  selector: 'app-doctor-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './doctor-detail.component.html',
  styleUrl: './doctor-detail.component.css'
})
export class DoctorDetailComponent implements OnInit {

  // add properties for doctorId
   DoctorsId!: string;

  // add properties for patients
  doctors$?: Observable<Doctor>; 

  // add constructor
  constructor(
    private doctorService: DoctorService,
    private route: ActivatedRoute
  ) { }

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

     // Get the doctor ID from the route parameters
    this.DoctorsId = this.route.snapshot.paramMap.get('id') || '';

    // Fetch doctor details (return a single Doctor object)
    this.doctors$ = this.doctorService.getDoctorById(this.DoctorsId);
  }

}
