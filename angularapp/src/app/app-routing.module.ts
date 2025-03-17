import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { UseraddfeedbackComponent } from './components/useraddfeedback/useraddfeedback.component';
import { AdminviewfeedbackComponent } from './components/adminviewfeedback/adminviewfeedback.component';
import { AdminvieworphanageComponent } from './components/adminvieworphanage/adminvieworphanage.component';
import { CreateorphanageComponent } from './components/createorphanage/createorphanage.component';
import { MydonationComponent } from './components/mydonation/mydonation.component';
import { UserviewfeedbackComponent } from './components/userviewfeedback/userviewfeedback.component';
import { ViewalldonationComponent } from './components/viewalldonation/viewalldonation.component';
import { AppComponent } from './app.component';
import { AdminnavComponent } from './components/adminnav/adminnav.component';
import { UsernavComponent } from './components/usernav/usernav.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ErrorComponent } from './components/error/error.component';
import { UservieworphanageComponent } from './components/uservieworphanage/uservieworphanage.component';
import { Error401Component } from './errorpages/error401/error401.component';
import { Error500Component } from './errorpages/error500/error500.component';
import { AuthguardGuard } from './components/authguard.guard';
import { Error409Component } from './errorpages/error409/error409.component';


const routes: Routes = [
  {path:'home',component : HomeComponent},
  {path:'register',component: RegistrationComponent},
  {path:'login',component: LoginComponent},
  {path:'useraddfeedback',component: UseraddfeedbackComponent,canActivate:[AuthguardGuard], data: { roles: ['User']} },
  {path:'adminviewfeedback',component: AdminviewfeedbackComponent,canActivate:[AuthguardGuard], data: { roles: ['Admin'] }},
  {path:'adminvieworphanage',component: AdminvieworphanageComponent,canActivate:[AuthguardGuard], data: { roles: ['Admin'] }},
  {path:'uservieworphanage',component: UservieworphanageComponent,canActivate:[AuthguardGuard], data: { roles: ['User'] }},
  {path:'createorphanage',component: CreateorphanageComponent, canActivate:[AuthguardGuard], data: { roles: ['Admin'] }},
  {path:'createorphanage/:id',component: CreateorphanageComponent, canActivate:[AuthguardGuard], data: { roles: ['Admin'] }},
  {path:'mydonation',component: MydonationComponent, canActivate:[AuthguardGuard], data: { roles: ['User'] }},
  {path:'userviewfeedback',component: UserviewfeedbackComponent, canActivate:[AuthguardGuard], data: { roles: ['User'] }},
  {path:'viewalldonation',component: ViewalldonationComponent, canActivate:[AuthguardGuard], data: { roles: ['Admin'] }},
  {path:'',redirectTo: '/home', pathMatch:'full'},
  {path:'parent',component: AppComponent},
  {path:'adminnavbar',component: AdminnavComponent},
  {path:'usernavbar',component: UsernavComponent},
  {path:'navbar',component: NavbarComponent},
  {path:'',redirectTo: '/login', pathMatch:'full'},
  {path:'error', component : ErrorComponent},
  { path: '401', component: Error401Component },
  { path: '500', component: Error500Component },
  { path: '409', component: Error409Component },
  {path:'**',component : ErrorComponent}
 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
