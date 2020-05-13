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
  registeredUser = {} as RegisterUserModel;
  token: string;
  uploadFileName: string;
  profileImage: File = null;

  constructor(private authService: AuthService,
              private router: Router,
              private toolBarService: ToolBarService) {
  }

  ngOnInit(): void {
    this.toolBarService.setTitle('Register');
  }


  onSubmit() {
    let formData: FormData = new FormData();
    formData.append('profileImage', this.profileImage);
    var postData = JSON.stringify(this.registeredUser);
    formData.append("postData",postData );
    console.log(formData);
    this.register(formData);
  }

  register(form : FormData) {
    this.authService.registrationFormData(form)
      .subscribe(data => {
        this.token = data.accessToken;
        this.authService.tokenObject = this.token;
        localStorage.setItem('accessToken', this.authService.tokenObject);
        this.router.navigate(['/profile']);
      });
  }
  onUploadImage(file) {
    this.profileImage = <File>file.target.files[0];
    this.uploadFileName = file.target.files[0].type.indexOf("image") !== -1 ? file.target.files[0].name : '';
  }
}
