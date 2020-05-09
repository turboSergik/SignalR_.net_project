import {Component, OnInit} from '@angular/core';
import {RegisterUserModel} from '../Interfaces/RegisterUserModel';
import {AuthService} from '../_services/auth.service';
import {ToolBarService} from '../_services/toolbar.service.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {

  roles: string[] = ['Employer', 'Student'];

  constructor(private authService: AuthService,
              private router: Router,
              private toolBarService: ToolBarService) {
  }

  registeredUser = {} as RegisterUserModel;
  token: string;

  onSubmit() {
    this.register();
  }

  ngOnInit(): void {
    this.toolBarService.setTitle('Register');
  }

  register() {
    this.authService.registration(this.registeredUser)
      .subscribe(data => {
        this.token = data.accessToken;
        this.authService.tokenObject = this.token;
        localStorage.setItem('accessToken', this.authService.tokenObject);
        this.router.navigate(['/profile']);
      });
  }
}
