import { Routes } from '@angular/router';
import { StaffListComponent } from './features/staff/staff-list/staff-list.component';
import { AddStaffComponent } from './features/staff/add-staff/add-staff.component';
import { EditStaffComponent } from './features/staff/edit-staff/edit-staff.component';
import { PatientListComponent } from './features/patient/patient-list/patient-list.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { EditPatientComponent } from './features/patient/edit-patient/edit-patient.component';
import { PatientDetailComponent } from './features/patient/patient-detail/patient-detail.component';

export const routes: Routes = [

    {
        path:"admin/staffs",
        component:StaffListComponent,
    },
    {
        path:"admin/staffs/add",
        component:AddStaffComponent,
    },
    {
        path:"admin/staffs/:id",
        component: EditStaffComponent, 
    },
    {
        path:"admin/patients",
        component: PatientListComponent,
    },
    {
        path:"admin/patients/add",
        component: AddPatientComponent,
    },
    {
        path:"admin/patients/:id",
        component: EditPatientComponent,
    },
    {
        path:"admin/patients/details/:id",
        component: PatientDetailComponent, 
    },
];
