import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-treatment-list',
  imports: [RouterModule, CommonModule, NgbPaginationModule, NgbAlertModule],
  templateUrl: './treatment-list.component.html',
  styleUrl: './treatment-list.component.css'
})
export class TreatmentListComponent {

}
