import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { SignOutComponent } from './sign-out/sign-out.component';
import { ProfileMenuComponent } from './profile-menu/profile-menu.component';
import { PageHeaderComponent } from './page-header/page-header.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

import { CustomerService } from './services/customer.service';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './utilities/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SignUpComponent,
    SignInComponent,
    ProfileComponent,
    SignOutComponent,
    EditProfileComponent,
    ProfileMenuComponent,
    PageHeaderComponent,
    ChangePasswordComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full'},
      { path: 'sign-up', component: SignUpComponent },
      { path: 'sign-in', component: SignInComponent },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'edit-profile', component: EditProfileComponent, canActivate: [AuthGuard] },
      { path: 'sign-out', component: SignOutComponent },
      { path: 'change-password', component: ChangePasswordComponent, canActivate: [AuthGuard]  }
    ])
  ],
  providers: [
    CustomerService,
    AuthService,
    AuthGuard,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
