import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UsernavComponent } from './components/usernav/usernav.component';
import { AdminnavComponent } from './components/adminnav/adminnav.component';
import { CreateorphanageComponent } from './components/createorphanage/createorphanage.component';
import { AdminvieworphanageComponent } from './components/adminvieworphanage/adminvieworphanage.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { MydonationComponent } from './components/mydonation/mydonation.component';
import { UserviewfeedbackComponent } from './components/userviewfeedback/userviewfeedback.component';
import { UseraddfeedbackComponent } from './components/useraddfeedback/useraddfeedback.component';
import { LoginComponent } from './components/login/login.component';
import { TokenInterceptor } from './interceptor/token.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ViewalldonationComponent } from './components/viewalldonation/viewalldonation.component';
import { AdminviewfeedbackComponent } from './components/adminviewfeedback/adminviewfeedback.component';
import { ErrorComponent } from './components/error/error.component';
import { UservieworphanageComponent } from './components/uservieworphanage/uservieworphanage.component';
import { Error401Component } from './errorpages/error401/error401.component';
import { Error500Component } from './errorpages/error500/error500.component';
import { ErrorInterceptor } from './interceptor/error.interceptor';
import { NgxPaginationModule } from 'ngx-pagination';
import { Error409Component } from './errorpages/error409/error409.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    UsernavComponent,
    AdminnavComponent,
    CreateorphanageComponent,
    AdminvieworphanageComponent,
    RegistrationComponent,
    HomeComponent,
    MydonationComponent,
    UserviewfeedbackComponent,
    UseraddfeedbackComponent,
    LoginComponent,
    ViewalldonationComponent,
    UservieworphanageComponent,
    ErrorComponent,
    AdminviewfeedbackComponent,
    Error401Component,
    Error500Component,
    Error409Component
  ],
  imports: [
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgxPaginationModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }