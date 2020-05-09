import {Component} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../_services/auth.service';
import {ToolBarService} from '../_services/toolbar.service.service';
import decode from 'jwt-decode';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  userName: string;
  password: string;
  token: string;
  role: string;
  decodedToken: any;

  constructor(private activatedRouter: ActivatedRoute,
              private router: Router,
              private toolBarService: ToolBarService,
              private http: HttpClient, private auth: AuthService,
  ) {
    this.toolBarService.setTitle('Login');
  }

  onSubmit() {
    this.auth.login(this.userName, this.password)
      .subscribe(data => {
        this.token = data.accessToken;
        this.auth.tokenObject = this.token;
        localStorage.setItem('accessToken', this.auth.tokenObject);
        this.decodedToken = decode(this.auth.getToken());
        this.role = this.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        this.router.navigate(['/profile']);
      });
  }
}
