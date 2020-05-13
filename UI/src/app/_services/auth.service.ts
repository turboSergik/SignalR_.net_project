import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegisterUserModel} from '../Interfaces/RegisterUserModel';
import {Router} from '@angular/router';
import {NgxPermissionsService} from 'ngx-permissions';

const URL_LOG = 'http://localhost:5000/Auth/Login';
const URL_REGISTER = 'http://localhost:5000/Auth/Registration';
const URL_PROFILE = 'http://localhost:5000/User';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

export interface AuthServiceResponse {
  accessToken: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  tokenObject: string = '';
  role: string;

  constructor(private http: HttpClient,
              private router: Router,
              private permissionsService: NgxPermissionsService) {
    this.isLoggedIn() && this.setUserRole();
  }

  login(username: string, password: string): Observable<AuthServiceResponse> {
    return this.http.post<AuthServiceResponse>(URL_LOG, {Username: username, Password: password}, httpOptions);
  }

  parseToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  };

  setUserRole() {
    const decodedToken = this.parseToken(this.getToken());

    this.permissionsService.loadPermissions([decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']]);
  }

  registration(register: RegisterUserModel) {
    return this.http.post<AuthServiceResponse>(URL_REGISTER, register, httpOptions);
  }

  registrationFormData(register: any): Observable<any> {
    return this.http.post<AuthServiceResponse>(URL_REGISTER, register);
  }

  public getToken(): string {
    return localStorage.getItem('accessToken');
  }

  public isLoggedIn(): boolean {
    return !!this.getToken();
  }

  public logout(): boolean {
    this.permissionsService.flushPermissions();
    localStorage.removeItem('accessToken');
    this.router.navigate(['/sign-in']);
    return true;
  }
}
