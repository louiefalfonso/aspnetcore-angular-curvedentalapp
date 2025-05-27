import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Treatment } from '../models/treatment.models';
import { Doctor } from '../../doctor/models/doctor.models';
import { TreatmentService } from '../services/treatment.service';

@Component({
  selector: 'app-treatment-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './treatment-detail.component.html',
  styleUrl: './treatment-detail.component.css'
})
export class TreatmentDetailComponent implements OnInit {

  // add properties 
  TreatmentsId!: string;
  treatments$? : Observable<Treatment>;
  doctors$?: Observable<Doctor[]>;

  // constructor
   constructor(
    private treatmentService: TreatmentService,
    private route: ActivatedRoute
  ) {}


   // implement ngOnInit lifecycle hook
  ngOnInit(): void {

    // get treatment Id from the route
    this.TreatmentsId = this.route.snapshot.paramMap.get('id') || '';

    // fetch treatment details & return single object
    this.treatments$ = this.treatmentService.getTreatmentById(this.TreatmentsId); 

    
    // Fetch doctor filtered by the treatment ID
    this.doctors$ = this.treatments$.pipe(
      map(treatment => treatment?.doctors || []),
      catchError(error => {
        console.error('Error fetching doctors:', error);
        return of([]);
      })
    );


  }
}
