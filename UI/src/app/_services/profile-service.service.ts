import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { ProfileDTO} from '../_models/DTO/ProfileDTO';


const URL_PROFILE ="http://localhost:5000/User";

@Injectable({
  providedIn: 'root'
})
export class ProfileServiceService {
  constructor(private http: HttpClient) {
  }

  getProfileUser(): Observable<ProfileDTO> {
    return this.http.get<ProfileDTO>(`${URL_PROFILE}/Profile`);
  }
}
