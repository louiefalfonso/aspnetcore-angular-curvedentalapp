import { Routes } from '@angular/router';
import { StaffListComponent } from './features/staff/staff-list/staff-list.component';
import { AddStaffComponent } from './features/staff/add-staff/add-staff.component';
import { EditStaffComponent } from './features/staff/edit-staff/edit-staff.component';

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
];
