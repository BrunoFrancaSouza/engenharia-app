import { Injectable } from '@angular/core';
import { CrudService } from '../crud/crud.service';
import { Permission } from 'src/app/models/Entities/Permission';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionService extends CrudService<Permission, number> {

  baseUrl = `${environment.apiEndpoint}/api/permission`;

  constructor(public http: HttpClient) {
    super(http, `${environment.apiEndpoint}/api/permission`);
  }

  getByRole(roleId: number): Observable<Permission[]> {
    var url = `${this.baseUrl}/GetByRole?roleId=${roleId}`;
    return this.http.get<Permission[]>(url)
  }


}
