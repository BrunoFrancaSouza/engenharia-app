import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, catchError, retry } from 'rxjs/operators';
import { Role } from 'src/app/models/Entities/Role';
import { Observable, throwError } from 'rxjs';
import { NotificationService } from '../notification/notification.service';
import { ErrorService } from '../errors/error.service';
import { CrudService } from '../crud/crud.service';
import { Permission } from 'src/app/models/Entities/Permission';
import { RoleUpdatePermissionsDto } from 'src/app/models/DTOs/RoleUpdatePermissionsDto';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends CrudService<Role, number> {

  baseUrl = environment.apiEndpoint + '/api/role';

  constructor(public http: HttpClient,
    private notificationService: NotificationService,
    private errorService: ErrorService
  ) {
    super(http, `${environment.apiEndpoint}/api/role`);
  }

  updatePermissions(roleUpdatePermissions: RoleUpdatePermissionsDto): Observable<Role> {
    return this.http.post<Role>(`${this.baseUrl}/UpdatePermissions`, roleUpdatePermissions);
  }

  // removePermissions(roleUpdatePermissions: RoleUpdatePermissionsDto): Observable<Role> {
  //   return this.http.post<Role>(`${this.baseUrl}/RemovePermissions`, roleUpdatePermissions);
  // }

}
