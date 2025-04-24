import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { Staff } from '../models/staff.models';
import { StaffService } from '../services/staff.service';
import { UpdateStaffRequest } from '../models/update-staff-request.models';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-edit-staff',
  imports: [RouterModule, FormsModule, CommonModule, NgbToastModule],
  templateUrl: './edit-staff.component.html',
  styleUrl: './edit-staff.component.css'
})
export class EditStaffComponent implements OnInit, OnDestroy {

  // add id property
  id : string | null = null;

  // add Subscription
  routeSubscription?: Subscription;
  editStaffSubscription?: Subscription;
  deleteStaffSubscription? : Subscription;

  // add patient object
  model?: Staff;

  // Add toast visibility property
  showToast: boolean = false;
  toastMessage: string = '';

  // add constructor and inject the necessary services
  constructor(
    private staffService: StaffService,
    private router: Router,
    private route : ActivatedRoute
  ) { } 

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    // get the id of the staff to edit
    this.routeSubscription = this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if (this.id) {
        // get the staff details
        this.editStaffSubscription = this.staffService.getStaffById(this.id).subscribe({
          next: (response) => {
            this.model = response;
          },
          error: (error) => {
            console.error(error);
          }
        });
      }
    });
  }

  // implement onFormSubmit
  onFormSubmit():void{
    
    // convert this model to request object
    if(this.model && this.id){
      var updateStaffRequest : UpdateStaffRequest ={
        firstName:  this.model?.firstName?? '',
        lastName:  this.model?.lastName?? '',
        staffRole:  this.model?.staffRole?? '',
        age:  this.model?.age?? '',
        email:  this.model?.email?? '',
        phone:  this.model?.phone?? '',
        sex:  this.model?.sex?? '',
        address:  this.model?.address?? '',
      }

      //pass this object to the service
      if(this.id){
        this.editStaffSubscription = this.staffService.updateStaff(this.id, updateStaffRequest)
        .subscribe({
          next: (response) => {
            this.showToast = true; 
            this.toastMessage = 'Staff Updated Successfully!';
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
    }
  }

  // implement onDelete
  onDelete():void{
    if(this.id){
    this.deleteStaffSubscription = this.staffService.deleteStaff(this.id).subscribe({
        next: (response) => {
          this.toastMessage = 'Staff Deleted Successfully!';
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
  }

   // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.editStaffSubscription?.unsubscribe();
    this.deleteStaffSubscription?.unsubscribe();
  }

}
