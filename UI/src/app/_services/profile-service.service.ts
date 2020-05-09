import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EmployerProfileDTO} from '../_models/DTO/EmployerProfileDTO';


const URL_PROFILE ="http://localhost:5000/User/";

@Injectable({
  providedIn: 'root'
})
export class ProfileServiceService {
  constructor(private http: HttpClient) {
  }

  getProfileUser(): Observable<EmployerProfileDTO> {
      return this.http.get<EmployerProfileDTO>(`${URL_PROFILE}EmployerProfile`);
  }
}
