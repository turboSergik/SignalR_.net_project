import {Component, OnInit} from '@angular/core';
import {ToolBarService} from './_services/toolbar.service.service';
import {AuthService} from './_services/auth.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string;
  isHome: boolean;

  constructor(private toolBarService: ToolBarService,
              public authService: AuthService,
              private router: Router) {
    this.router.events.subscribe(() => {
      this.isHome = this.router.url === '/';
    });
  }

  ngOnInit() {
    this.toolBarService.getTitle()
      .subscribe((title: string) => {
        this.title = title;
      });
  }
}
