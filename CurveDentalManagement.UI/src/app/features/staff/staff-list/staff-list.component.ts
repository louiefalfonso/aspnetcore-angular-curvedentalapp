import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Staff } from '../models/staff.models';
import { Observable, of } from 'rxjs';
import { StaffService } from '../services/staff.service';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { StaffChartComponent } from '../staff-chart/staff-chart.component';
import { map, catchError } from 'rxjs/operators';


@Component({
  selector: 'app-staff-list',
  imports: [RouterModule, CommonModule, NgbPaginationModule, StaffChartComponent, NgbAlertModule],
  templateUrl: './staff-list.component.html',
  styleUrls: ['./staff-list.component.css']
})
export class StaffListComponent implements OnInit {

  staffs$?: Observable<Staff[]>;
  allStaffs: Staff[] = [];

  // For sorting, filtering & pagination
  totalCount: number = 0;
  list: number[] = [];
  pageNumber = 1;
  pageSize = 10;

  // Add constructor
  constructor(
    private staffService: StaffService,
    private route: ActivatedRoute
  ) { }

  // Implement ngOnInit lifecycle hook
  ngOnInit(): void {
    this.loadAllStaffs();

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

  // Implement loadAllStaffs method for chart display
  loadAllStaffs(): void {
    this.staffService.getAllStaffs().pipe(
      map((staffs: Staff[] | null) => staffs ?? []), 
      catchError(() => of([])) 
    ).subscribe((staffs) => {
      this.allStaffs = staffs; 
    });
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

  // Implement getPage
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

  // Implement getNextPage
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