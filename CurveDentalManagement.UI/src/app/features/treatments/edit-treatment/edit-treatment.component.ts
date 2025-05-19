import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { Treatment } from '../models/treatment.models';
import { TreatmentService } from '../services/treatment.service';
import { DoctorService } from '../../doctor/services/doctor.service';
import { UpdateTreatmentRequest } from '../models/update-treatment-request.models';

@Component({
  selector: 'app-edit-treatment',
  imports: [RouterModule, FormsModule, CommonModule, NgbToastModule],
  templateUrl: './edit-treatment.component.html',
  styleUrl: './edit-treatment.component.css'
})
export class EditTreatmentComponent implements OnInit, OnDestroy {

// add id property
id : string | null = null;
model?: Treatment;
doctors$? : Observable<Doctor[]>
selectedDoctor?: string[];

// add Subscription
routeSubscription?: Subscription;
getTreatmentSubscription?: Subscription;
editTreatmentSubscription?: Subscription;
deleteTreatmentSubscription? : Subscription;

// Add toast visibility property
showToast: boolean = false;
toastMessage: string = '';

// add constructor and inject the necessary services
constructor(
    private treatmentService: TreatmentService,
    private doctorService: DoctorService,
    private router: Router,
    private route : ActivatedRoute,
){}

// add treatment object
treatment?:Treatment;
  
  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    //get all doctors
    this.doctors$ = this.doctorService.getAllDoctors();

    // get all id of the treatment to edit
   this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) =>{
        this.id = params.get('id');

        if(this.id){
         this.getTreatmentSubscription = this.treatmentService.getTreatmentById(this.id).subscribe({
            next: (response) =>{
              this.model = response;
              this.selectedDoctor = response.doctors.map(x => x.id);
            },
            error: (error) => {
              console.error(error);
            }
          })
        }
      }
    })
  }

  // implement onSubmit 
  onFormSubmit(): void {
      // convert this method to request object
       if(this.model && this.id){
        var updateTreatmentRequest : UpdateTreatmentRequest = {
          treatmentName: this.model.treatmentName?? '',
          treatmentCode: this.model.treatmentCode?? '',
          description: this.model.description?? '',
          cost: this.model.cost?? '',
          duration: this.model.duration?? '',
          insuranceCoverage: this.model.insuranceCoverage?? '',
          insuranceCoverageAmount: this.model.insuranceCoverageAmount?? '',
          followUpCare: this.model.followUpCare?? '',
          riskBenefits: this.model.riskBenefits?? '',
          indications: this.model.indications?? '',
          doctors:this.selectedDoctor??[]
        };

        // pass this object to the service
        if(this.id){
          this. editTreatmentSubscription = this.treatmentService.updateTreatment(this.id, updateTreatmentRequest)
          .subscribe({
             next: (response) => {
              this.showToast = true; 
              this.toastMessage = 'Treatment Updated Successfully!';
              setTimeout(() => {
                this.showToast = false;
                this.router.navigate(['/admin/treatments']);
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
      this.deleteTreatmentSubscription= this.treatmentService.deleteTreatment(this.id).subscribe({
          next: (response) => {
            this.toastMessage = 'Treatment Deleted Successfully!';
            this.showToast = true;
            setTimeout(() => {
              this.showToast = false; 
              this.router.navigate(['/admin/treatments']);
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
    this.getTreatmentSubscription?.unsubscribe();
    this.editTreatmentSubscription?.unsubscribe();
    this.deleteTreatmentSubscription?.unsubscribe();
  }

}
