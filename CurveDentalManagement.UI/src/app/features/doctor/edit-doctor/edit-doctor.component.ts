import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Doctor } from '../models/doctor.models';
import { DoctorService } from '../services/doctor.service';
import { UpdateDoctorRequest } from '../models/update-doctor-request.models';

@Component({
  selector: 'app-edit-doctor',
  imports: [RouterModule, FormsModule, CommonModule, NgbToastModule],
  templateUrl: './edit-doctor.component.html',
  styleUrl: './edit-doctor.component.css'
})
export class EditDoctorComponent implements OnInit, OnDestroy {

// add id property
id : string | null = null;

 // add Subscription
routeSubscription?: Subscription;
editDoctorSubscription?: Subscription;
deleteDoctorSubscription? : Subscription;

// add patient object
model?: Doctor;

// Add toast visibility property
showToast: boolean = false;
toastMessage: string = '';

// add constructor and inject the necessary services
constructor(
  private doctorService: DoctorService,
  private router: Router,
  private route : ActivatedRoute
) { } 
  

// implement ngOnInit lifecycle hook
ngOnInit(): void {
  // get the id of the doctor to edit
  this. routeSubscription = this.route.paramMap.subscribe(params=>{
    this.id = params.get('id');
    if (this.id) {
      // get the doctor details
      this.editDoctorSubscription = this.doctorService.getDoctorById(this.id).subscribe({
        next: (response) => {
          this.model = response;
        },
        error: (error) => {
          console.error(error);
        }
      });
    }
  })
}

// implement onSubmit 
onFormSubmit(): void {
  // convert this method to request object
  if(this.model && this.id){
    var updateDoctorRequest : UpdateDoctorRequest = {
      firstName: this.model.firstName?? '',
      lastName: this.model.lastName?? '',
      email: this.model.email?? '',
      contactNumber: this.model.contactNumber?? '',
      specialization: this.model.specialization?? '',
      department: this.model.department?? '',
      schedule: this.model.schedule?? '',
      licenseNumber: this.model.licenseNumber?? '',
      yearsOfExperience: this.model.yearsOfExperience?? '',
      dentalSchool: this.model.dentalSchool?? '',
      officeAddress: this.model.officeAddress?? '',
      emergencyContact: this.model.emergencyContact?? ''
    }

    //pass this object to the service
    if(this.id){
      this.editDoctorSubscription = this.doctorService.updateDoctor(this.id, updateDoctorRequest)
      .subscribe({
        next: (response) => {
          this.showToast = true; 
          this.toastMessage = 'Doctor Updated Successfully!';
          setTimeout(() => {
            this.showToast = false;
            this.router.navigate(['/admin/doctors']);
          }, 2000);
        },
        error: (error) => {
          console.error(error);
        }
      })
    }
  }
}


// implement onDelete
onDelete():void{
    if(this.id){
    this.deleteDoctorSubscription = this.doctorService.deleteDoctor(this.id).subscribe({
        next: (response) => {
          this.toastMessage = 'Doctor Deleted Successfully!';
          this.showToast = true;
          setTimeout(() => {
            this.showToast = false; 
            this.router.navigate(['/admin/doctors']);
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
    this.editDoctorSubscription?.unsubscribe();
    this.deleteDoctorSubscription?.unsubscribe();
  }

}
