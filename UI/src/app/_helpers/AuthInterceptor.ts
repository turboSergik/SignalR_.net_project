import {Injectable} from '@angular/core';
import {HttpEvent,HttpInterceptor,HttpHandler,HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    constructor(public Auth: AuthService){}


      intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
                  setHeaders: {
                    'Authorization' : `Bearer ${this.Auth.getToken()}`
                  }
                });
        return next.handle(request);
      }




}


