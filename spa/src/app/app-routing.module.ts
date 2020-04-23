import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { RolesComponent } from './components/roles/roles.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'roles', component: RolesComponent },
  // {
  //   path: 'user',
  //   component: RegistrationComponent,
  //   children: [
  //     { path: 'login', component: LoginComponent },
  //     { path: 'registration', component: RegistrationComponent }
  //   ]
  // },
  { path: '', pathMatch: 'full', redirectTo: '/home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
