import {Component, OnInit} from '@angular/core';
import {ToolBarService} from './_services/toolbar.service.service';
import {AuthService} from './_services/auth.service';
import {ProfileServiceService} from './_services/profile-service.service';
import {EmployerProfileDTO} from './_models/DTO/EmployerProfileDTO';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Job App';
  loggedUser = {} as EmployerProfileDTO;

  constructor(private toolBarService: ToolBarService,
              public authService: AuthService,
              public profileService: ProfileServiceService) {
  }

  ngOnInit() {
    this.toolBarService.getTitle()
      .subscribe((title: string) => {
        this.title = title;
        this.profileService.getProfileUser().subscribe(user => {
          console.log(user);
          if (this.authService.isLoggedIn()) {
            this.loggedUser = user;
          }
        });
      });
  }
}
