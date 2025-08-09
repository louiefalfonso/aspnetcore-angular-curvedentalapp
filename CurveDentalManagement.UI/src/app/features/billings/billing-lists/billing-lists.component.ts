import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { Billing } from '../models/billing.models';
import { Observable } from 'rxjs';
import { BillingService } from '../services/billing.service';

@Component({
  selector: 'app-billing-lists',
  imports: [RouterModule, CommonModule, NgbPaginationModule, NgbAlertModule],
  templateUrl: './billing-lists.component.html',
  styleUrl: './billing-lists.component.css'
})
export class BillingListsComponent implements OnInit{

  // Define the observable for appointments
  billings$? : Observable<Billing[]>;

  // For sorting, filtering & pagination
  totalCount: number = 0;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

  // Add constructor
 constructor(
  private billingService: BillingService,
  private route: ActivatedRoute
  ) { }

  // Implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.billingService.getBillingCount().subscribe({
      next:(value) =>{
        this.totalCount = value;
        this.list = new Array(Math.ceil(value / this.pageSize))
        
        this.billings$ = this.billingService.getAllBillings(
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
     this.billings$ = this.billingService.getAllBillings(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.billings$ = this.billingService.getAllBillings(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
     this.billings$ = this.billingService.getAllBillings(undefined, sortBy, sortDirection);
  }

  // Implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.billings$ = this.billingService.getAllBillings(
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
    this.billings$ = this.billingService.getAllBillings(
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
     this.billings$ = this.billingService.getAllBillings(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }


}
