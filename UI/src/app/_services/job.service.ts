import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PaginatedRequest} from '../_models/PaginatedRequest';
import {JobDTO} from '../_models/DTO/JobDTO';
import {PagedResult} from '../_models/PagedResult';
import {CategoryDTO} from '../_models/DTO/CategoryDTO';
import {CityDTO} from '../_models/DTO/CityDTO';
import {JobType} from '../_models/JobType';

const urlJob = 'http://localhost:5000/Job';



@Injectable({
  providedIn: 'root'
})

export class JobService {

  constructor(private http: HttpClient) {

  }

  getAllJobs() : Observable<JobDTO[]>{
    return this.http.get<JobDTO[]>(`${urlJob}`);
  }


  getJobById(id: number) {
    return this.http.get<JobDTO>(`${urlJob}/${id}`);
  }

  getAllJobPaginated(paginatedRequest: PaginatedRequest): Observable<PagedResult<JobDTO>> {
    return this.http.post<PagedResult<JobDTO>>(`${urlJob}/PagePerTable`, paginatedRequest);
  }

  getAllJobPaginatedUser(paginatedRequest: PaginatedRequest): Observable<PagedResult<JobDTO>> {
    return this.http.post<PagedResult<JobDTO>>(`${urlJob}/Profile`, paginatedRequest);
  }

  getCategories(): Observable<CategoryDTO[]>{
    return  this.http.get<CategoryDTO[]>(`${urlJob}/Categories`);
  }

  getCity(): Observable<CityDTO[]>{
    return  this.http.get<CityDTO[]>(`${urlJob}/City`);
  }

  addjob(job: JobDTO): Observable<any> {
    return  this.http.post(`${urlJob}`, job);
  }

  AddJobFormData(formData : any) : Observable<any> {
    return  this.http.post(`${urlJob}/Post`, formData);
  }


  deleteJob(id: number): Observable<any>{
    return  this.http.delete(`${urlJob}/${id}`);
  };

  editJob(formData: any,id: number): Observable<any>{
    return  this.http.put(`${urlJob}/Update/${id}`, formData);
  };

  getJobsByType(filter: PaginatedRequest,id:number) : Observable<PagedResult<JobDTO>>{
    return  this.http.post<PagedResult<JobDTO>>(`${urlJob}/Type/${id}`, filter);
  }

  getJobsTypes() : Observable<JobType[]>{
    return  this.http.get<JobType[]>(`${urlJob}/Types`);
  }

}
