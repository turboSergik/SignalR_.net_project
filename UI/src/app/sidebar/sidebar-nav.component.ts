import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import {AdvertService} from '../_services/advert.service';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {UpdateAdvertComponent} from '../advert/update-advert/update-advert.component';

@Component({
  selector: 'app-sidebar-nav',
  templateUrl: './sidebar-nav.component.html',
  styleUrls: ['./sidebar-nav.component.css']
})
export class SidebarNavComponent {
  serviceAuth : AuthService;
  advertService: AdvertService;
  dialogRef: MatDialogRef<any>;
  constructor(private authService :AuthService,  advertService : AdvertService, public dialog: MatDialog) {
    this.serviceAuth = authService;
    this.advertService = advertService;
  }

  onAddAdvert(){
      this.dialogRef = this.dialog.open(UpdateAdvertComponent);
  }
}
