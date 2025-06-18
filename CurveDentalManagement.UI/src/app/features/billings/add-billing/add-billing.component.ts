import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbToastModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { AddBillingRequest } from '../models/add-billing-request.models';
import { Observable, Subscription } from 'rxjs';
import { Treatment } from '../../treatments/models/treatment.models';
import { Patient } from '../../patient/models/patient.models';

@Component({
  selector: 'app-add-billing',
  imports: [CommonModule, FormsModule, RouterModule, NgbToastModule, NgbAlertModule],
  templateUrl: './add-billing.component.html',
  styleUrl: './add-billing.component.css'
})
export class AddBillingComponent implements OnInit, OnDestroy{

  // add model
  //model: AddBillingRequest;
  patients$? : Observable<Patient[]>;
  treatments$?: Observable<Treatment[]>;
  
  // add unsubcribe from observables
  private addBillingSubscription ?: Subscription;

  // add toast visibility property
  showToast: boolean = false;

  // add constructor
  constructor() { }



  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

}
