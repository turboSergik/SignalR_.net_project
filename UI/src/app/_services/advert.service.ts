import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable, Subject} from 'rxjs';
import {AdvertDTO} from '../_models/DTO/AdvertDTO';
import {PaginatedRequest} from '../_models/PaginatedRequest';
import {PagedResult} from '../_models/PagedResult';
import {JobDTO} from '../_models/DTO/JobDTO';



const URL_ADVERT = "http://localhost:5000/Advert"

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient, private router: Router) {
  }

  popUp = new Subject();
  added = new Subject();

  onPopup() {
    return this.popUp.asObservable();
  }

  onAddAdvert() {
    return this.added.asObservable();
  }

  getAdvertForStudent(filter: PaginatedRequest) : Observable<PagedResult<AdvertDTO>>{
    return  this.http.post<PagedResult<AdvertDTO>>(`${URL_ADVERT}/Student/Adverts`, filter);
  }

  postStudentAdverts(advert: AdvertDTO): Observable<any> {
    return this.http.post(`${URL_ADVERT}`, advert);
  }

  getPostedAdvert(advertId: number): Observable<AdvertDTO> {
    return this.http.get<AdvertDTO>(`${URL_ADVERT}/${advertId}`);
  }

  putAdvert(advert: AdvertDTO, advertId: number): Observable<any> {
    return this.http.put<any>(`${URL_ADVERT}/Update/${advertId}`, advert);
  }

  deleteAdvert(id: number): Observable<any> {
    return this.http.delete(`${URL_ADVERT}/${id}`);
  }




  getAdverts(): Observable<AdvertDTO[]> {
    return this.http.get<AdvertDTO[]>(`${URL_ADVERT}`);
  }
}
