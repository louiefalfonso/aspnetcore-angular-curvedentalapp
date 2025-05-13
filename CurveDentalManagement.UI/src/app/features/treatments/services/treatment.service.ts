import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { AddTreatmentRequest } from '../models/add-treatment-request.models';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Treatment } from '../models/treatment.models';
import { Doctor } from '../../doctor/models/doctor.models';
import { UpdateTreatmentRequest } from '../models/update-treatment-request.models';

@Injectable({
  providedIn: 'root'
})
export class TreatmentService {

  // add constructor
  constructor(private http:HttpClient) { }

  // add new treatment
  addNewTreatment(model:AddTreatmentRequest): Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/treatments`, model);
  }

  // get all treatments
  getAllTreatments(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,):Observable<Treatment[]>{

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

    return this.http.get<Treatment[]>(`${environment.apiBaseUrl}/treatments`,{
      params: params
      });

  }

   // get treatment by id
   getTreatmentById(id: string): Observable<Treatment>{
    return this.http.get<Treatment>(`${environment.apiBaseUrl}/treatments/${id}`);
  }

  // update treatment
  updateTreatment(id: string, updateTreatmentRequest: UpdateTreatmentRequest): Observable<Treatment>{
      return this.http.put<Treatment>(`${environment.apiBaseUrl}/treatments/${id}`, updateTreatmentRequest);
  }

  // delete treatment
  deleteTreatment(id: string): Observable<Treatment>{
    return this.http.delete<Treatment>(`${environment.apiBaseUrl}/treatments/${id}`);
  }

    // get treatment count
    getTreatmentCount(): Observable<number> {
      return this.http.get<number>(`${environment.apiBaseUrl}/treatments/count`);
    }
}
