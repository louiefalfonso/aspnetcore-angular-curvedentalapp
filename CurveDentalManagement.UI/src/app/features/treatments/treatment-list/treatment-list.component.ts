import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { Treatment } from '../models/treatment.models';
import { TreatmentService } from '../services/treatment.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-treatment-list',
  imports: [RouterModule, CommonModule, NgbPaginationModule, NgbAlertModule],
  templateUrl: './treatment-list.component.html',
  styleUrl: './treatment-list.component.css'
})
export class TreatmentListComponent implements OnInit{

  // Define the observable for treatments
  treatments$? : Observable<Treatment[]>;

  // For sorting, filtering & pagination
  totalCount: number = 0;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

 // Add constructor
 constructor(
  private treatmentService: TreatmentService,
  private route: ActivatedRoute
  ) { }

  // Implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.treatmentService.getTreatmentCount()
    .subscribe({
      next:(value) =>{
        this.totalCount = value;
        this.list = new Array(Math.ceil(value / this.pageSize))
 
        this.treatments$ = this.treatmentService.getAllTreatments(
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
    this.treatments$ = this.treatmentService.getAllTreatments(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.treatments$ = this.treatmentService.getAllTreatments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.treatments$ = this.treatmentService.getAllTreatments(undefined, sortBy, sortDirection);
  }

  // Implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.treatments$ = this.treatmentService.getAllTreatments(
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
    this.treatments$ = this.treatmentService.getAllTreatments(
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
    this.treatments$ = this.treatmentService.getAllTreatments(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

}
