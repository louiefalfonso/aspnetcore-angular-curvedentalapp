import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Staff } from '../models/staff.models';
import { Observable } from 'rxjs';
import { StaffService } from '../services/staff.service';

@Component({
  selector: 'app-staff-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './staff-list.component.html',
  styleUrl: './staff-list.component.css'
})
export class StaffListComponent implements OnInit{

     // use asnyc pipe instead of subscription
     staffs$?: Observable<Staff[]>;

     // for sorting, filtering & pagination
     totalCount?: number;
     list: number[] = [];
     pageNumber = 1;
     pageSize = 10;
   
     // add constructor
     constructor(
       private staffService: StaffService,
       private route: ActivatedRoute
     ) { }


  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.staffService.getStaffCount()
   .subscribe({
      next: (value) => {
        this.totalCount = value;
         this.list = new Array(Math.ceil(value / this.pageSize))

         this.staffs$ = this.staffService.getAllStaffs(
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
    this.staffs$ = this.staffService.getAllStaffs(query);
  }

  //implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = ''; 
    this.pageNumber = 1; 
    this.staffs$ = this.staffService.getAllStaffs(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    ); 
  }

  // implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.staffs$ = this.staffService.getAllStaffs(undefined, sortBy, sortDirection);
  }

  // implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;

    this.staffs$ = this.staffService.getAllStaffs(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }

  
  // implement getNextPage
  getNextPage() {
    if (this.pageNumber + 1 > this.list.length) {
      return;
    }

    this.pageNumber += 1;
    this.staffs$ = this.staffService.getAllStaffs(
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
    this.staffs$ = this.staffService.getAllStaffs(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize
    );
  }


}
