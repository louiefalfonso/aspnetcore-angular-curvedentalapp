import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddStaffRequest } from '../models/add-staff-request.models';
import { Subscription } from 'rxjs';
import { StaffService } from '../services/staff.service';
import { HttpClient } from '@angular/common/http';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-staff',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule],
  templateUrl: './add-staff.component.html',
  styleUrl: './add-staff.component.css'
})
export class AddStaffComponent implements OnDestroy{

  // add model
  model: AddStaffRequest;

  // add unsubcribe from observables
  private addStaffSubscription ?: Subscription;

   // Add toast visibility property
   showToast: boolean = false;

  // add constructor
  constructor(
    private staffService : StaffService,
    private http: HttpClient,
    private router: Router){
    this.model = {
      firstName: '',
      lastName: '',
      staffRole: '',
      age: '',
      email: '',
      phone: '',
      sex: '',
      address: '',
    }
   }

  // add onFormSubmit
  onFormSubmit() {
    this.addStaffSubscription = this.staffService.addNewStaff(this.model)
      .subscribe({
        next: (response) => {
          this.showToast = true; 
          setTimeout(() => {
            this.showToast = false;
            this.router.navigate(['/admin/staffs']);
          }, 2000);
        },
        error: (error) => {
          console.error(error);
        }
      });
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addStaffSubscription?.unsubscribe();
  }

}
