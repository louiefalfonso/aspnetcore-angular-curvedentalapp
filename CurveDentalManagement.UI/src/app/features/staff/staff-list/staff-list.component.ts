import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Staff } from '../models/staff.models';
import { Observable, of } from 'rxjs';
import { StaffService } from '../services/staff.service';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { StaffChartComponent } from '../staff-chart/staff-chart.component';
import { map, catchError } from 'rxjs/operators';


@Component({
  selector: 'app-staff-list',
  imports: [RouterModule, CommonModule, NgbPaginationModule, StaffChartComponent],
  templateUrl: './staff-list.component.html',
  styleUrls: ['./staff-list.component.css']
})
export class StaffListComponent implements OnInit {

  // Use async pipe instead of subscription
  staffs$?: Observable<Staff[]>;

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
    this.loadStaffs();
  }

  loadStaffs(query?: string, sortBy?: string, sortDirection?: string): void {
      this.staffService.getStaffCount().subscribe({
        next: (value) => {
          this.totalCount = value || 0; 
          this.list = new Array(Math.ceil(this.totalCount / this.pageSize));
    
          this.staffs$ = this.staffService.getAllStaffs(
            query,
            sortBy,
            sortDirection,
            this.pageNumber,
            this.pageSize
          ).pipe(
            map((staffs: Staff[] | null) => staffs ?? []),
            catchError(() => of([]))
          );
        }
      });
  }

  // Implement search
  onSearch(query: string) {
    this.pageNumber = 1; 
    this.loadStaffs(query);
  }

  // Implement reset
  onReset(queryText: HTMLInputElement): void {
    queryText.value = '';
    this.pageNumber = 1;
    this.loadStaffs();
  }

  // Implement sorting
  sort(sortBy: string, sortDirection: string) {
    this.pageNumber = 1; 
    this.loadStaffs(undefined, sortBy, sortDirection);
  }

  // Implement getPage
  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.loadStaffs();
  }

  // Implement getNextPage
  getNextPage() {
    if (this.pageNumber + 1 > this.list.length) {
      return;
    }
    this.pageNumber += 1;
    this.loadStaffs();
  }

  // Implement getPrevPage
  getPrevPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.loadStaffs();
  }
}