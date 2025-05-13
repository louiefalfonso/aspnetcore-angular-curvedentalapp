import { Routes } from '@angular/router';
import { StaffListComponent } from './features/staff/staff-list/staff-list.component';
import { AddStaffComponent } from './features/staff/add-staff/add-staff.component';
import { EditStaffComponent } from './features/staff/edit-staff/edit-staff.component';
import { PatientListComponent } from './features/patient/patient-list/patient-list.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { EditPatientComponent } from './features/patient/edit-patient/edit-patient.component';
import { PatientDetailComponent } from './features/patient/patient-detail/patient-detail.component';
import { DoctorListComponent } from './features/doctor/doctor-list/doctor-list.component';
import { AddDoctorComponent } from './features/doctor/add-doctor/add-doctor.component';
import { EditDoctorComponent } from './features/doctor/edit-doctor/edit-doctor.component';
import { DoctorDetailComponent } from './features/doctor/doctor-detail/doctor-detail.component';
import { TreatmentListComponent } from './features/treatments/treatment-list/treatment-list.component';
import { AddTreatmentComponent } from './features/treatments/add-treatment/add-treatment.component';
import { EditTreatmentComponent } from './features/treatments/edit-treatment/edit-treatment.component';
import { TreatmentDetailComponent } from './features/treatments/treatment-detail/treatment-detail.component';

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
    {
        path:"admin/doctors",
        component: DoctorListComponent,
    },
    {
        path:"admin/doctors/add",
        component: AddDoctorComponent,
    },
    {
        path:"admin/doctors/:id",
        component: EditDoctorComponent,
    },
    {
        path:"admin/doctors/details/:id",
        component: DoctorDetailComponent,
    },
    {
        path:"admin/treatments",
        component: TreatmentListComponent,
    },
    {
        path:"admin/treatments/add",
        component: AddTreatmentComponent,
    },
    {
        path:"admin/treatments/:id",
        component: EditTreatmentComponent,
    },
    {
        path:"admin/treatments/details/:id",
        component: TreatmentDetailComponent,
    },
];
