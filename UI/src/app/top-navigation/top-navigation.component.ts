import {Component, Input, OnInit} from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {ProfileServiceService} from '../_services/profile-service.service';
import {ProfileDTO} from '../_models/DTO/ProfileDTO';
import {NgxPermissionsService} from 'ngx-permissions';


@Component({
  selector: 'app-top-navigation',
  templateUrl: './top-navigation.component.html',
  styleUrls: ['./top-navigation.component.css']
})
export class TopNavigationComponent implements OnInit {
  loggedUser = {} as ProfileDTO;
  @Input() isHome: boolean;
  @Input() title: string;

  constructor(public authService: AuthService,
              private permissionsService: NgxPermissionsService,
              private profileService: ProfileServiceService) {
  }

  ngOnInit() {
    this.permissionsService.permissions$.subscribe(() => {
      this.authService.isLoggedIn() && this.setLoggedUser();
    });
  }

  setLoggedUser() {
    this.profileService.getProfileUser().subscribe(
      user => {
      if (this.authService.isLoggedIn()) {
        this.loggedUser = user;
      }
    });
  }
}
