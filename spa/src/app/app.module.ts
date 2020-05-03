import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Necessário para o Two Way Data Binding
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { HomeComponent } from './components/home/home.component';
import { LoaderComponent } from './components/loader/loader.component';

import { LoaderService } from './services/loader/loader.service';

import { MaterialModule } from "./material/material.module";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';

import { FlexLayoutModule } from '@angular/flex-layout';
import { GlobalHttpInterceptorService } from './interceptors/http/global-http-Interceptor.service';
import { RolesComponent } from './components/roles/roles.component';
import { RoleDetailsComponent } from './components/roles/role-details/role-details.component';
import { RolesPermissionsComponent } from './components/roles/roles-permissions/roles-permissions.component';

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      RegistrationComponent,
      HomeComponent,
      LoaderComponent,
      RolesComponent,
      RoleDetailsComponent,
      RolesPermissionsComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      ToastrModule.forRoot({
         preventDuplicates: true,
         countDuplicates: true,
         resetTimeoutOnDuplicate: true
      }),
      ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
      MaterialModule,
      NgbModule,
      FlexLayoutModule,
      NgxPaginationModule,
   ],
   entryComponents: [
      RoleDetailsComponent // Put too in declarations section (Don't know why ... )
   ],
   providers: [
      LoaderService,
      { provide: HTTP_INTERCEPTORS, useClass: GlobalHttpInterceptorService, multi: true }
   ],
   bootstrap: [AppComponent]
})
export class AppModule { }