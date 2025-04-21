import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { AddStaffRequest } from '../models/add-staff-request.models';
import { Observable } from 'rxjs';
import { Staff } from '../models/staff.models';
import { UpdateStaffRequest } from '../models/update-staff-request.models';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

   // add constructor
   constructor(private http: HttpClient) { }

  // add new staff
  addNewStaff(model: AddStaffRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/staffs`, model);
  }

  // get all patients
  getAllStaffs(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,):Observable<Staff[]>{

    let params = new HttpParams();
    
    if (query) {
      params = params.set('query', query)
    }

    if (sortBy) {
      params = params.set('sortBy', sortBy)
    }

    if (sortDirection) {
      params = params.set('sortDirection', sortDirection)
    }

    if (pageNumber) {
      params = params.set('pageNumber', pageNumber)
    }

    if (pageSize) {
      params = params.set('pageSize', pageSize)
    }

    return this.http.get<Staff[]>(`${environment.apiBaseUrl}/staffs`,{
      params: params
      });

  }

   // get staff by id
   getStaffById(id: string): Observable<Staff>{
    return this.http.get<Staff>(`${environment.apiBaseUrl}/staffs/${id}`);
  }

  // update staff
  updateStaff(id: string, updateStaffRequest: UpdateStaffRequest): Observable<Staff>{
    return this.http.put<Staff>(`${environment.apiBaseUrl}/staffs/${id}`, updateStaffRequest);
  }

   // delete staff
   deleteStaff(id: string): Observable<Staff>{
    return this.http.delete<Staff>(`${environment.apiBaseUrl}/staffs/${id}`);
  }

  // get staff count
  getStaffCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/staffs/count`);
  }
}
