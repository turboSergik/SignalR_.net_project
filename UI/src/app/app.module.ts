import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {JobComponent} from './job/job.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';
import {HttpClientModule} from '@angular/common/http';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatButtonModule} from '@angular/material/button';
import {FlexLayoutModule} from '@angular/flex-layout';
import {JobService} from './_services/job.service';
import {MatInputModule} from '@angular/material/input';
import {FormsModule} from '@angular/forms';
import {AuthService} from './_services/auth.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {AuthInterceptor} from './_helpers/AuthInterceptor';
import {LayoutModule} from '@angular/cdk/layout';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {MatRadioModule} from '@angular/material/radio';
import {SidebarNavComponent} from './sidebar/sidebar-nav.component';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatMenuModule} from '@angular/material/menu';
import {RegistrationFormComponent} from './registration/registration-form.component';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {SignInComponent} from './sign-in/sign-in.component';
import {JobFilterComponent} from './job/job-filter/job-filter.component';
import {UserProfileComponent} from './user-profile/user-profile.component';
import {UpdateJobComponent} from './user-profile/update-job/update-job.component';
import {ProfileInfoComponent} from './user-profile/profile-info/profile-info.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {PopUpComponent} from './user-profile/pop-up/pop-up.component';
import {AdvertComponent} from './advert/advert.component';
import {PopUpAdvertComponent} from './advert/pop-up-advert/pop-up-advert.component';
import {UpdateAdvertComponent} from './advert/update-advert/update-advert.component';
import {ReactiveFormsModule} from '@angular/forms';
import {HomeComponent} from './home/home.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {ViewJobComponent} from './home/view-job/view-job.component';
import {NgxPermissionsModule} from 'ngx-permissions';
import { TopNavigationComponent } from './top-navigation/top-navigation.component';
import { ChatComponent } from './chat/chat.component';

@NgModule({
  declarations: [
    AppComponent,
    JobComponent,
    SidebarNavComponent,
    RegistrationFormComponent,
    SignInComponent,
    JobFilterComponent,
    UserProfileComponent,
    UpdateJobComponent,
    ProfileInfoComponent,
    PopUpComponent,
    AdvertComponent,
    PopUpAdvertComponent,
    UpdateAdvertComponent,
    HomeComponent,
    ViewJobComponent,
    TopNavigationComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatToolbarModule,
    MatCardModule,
    HttpClientModule,
    MatGridListModule,
    MatButtonModule,
    FlexLayoutModule,
    MatInputModule,
    FormsModule,
    LayoutModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatRadioModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatDialogModule,
    MatMenuModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    NgxPermissionsModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    JobService,
    AuthService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
