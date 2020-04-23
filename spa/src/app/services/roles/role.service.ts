import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, catchError, retry } from 'rxjs/operators';
import { Role } from 'src/app/models/Entities/Role';
import { Observable, throwError } from 'rxjs';
import { NotificationService } from '../notification/notification.service';
import { ErrorService } from '../errors/error.service';
import { CrudService } from '../crud/crud.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends CrudService<Role, number> {

  baseUrl = environment.apiEndpoint + '/role';

  constructor(public http: HttpClient,
    private notificationService: NotificationService,
    private errorService: ErrorService
  ) {
    super(http, `${environment.apiEndpoint}/role`);
  }

  // getAll(): Observable<Role[]> {
  //   var url = `${this.baseUrl}/GetAll`;
  //   return this.http.get(url).pipe(
  //     map((response: any) => {
  //       // console.log('response', response)
  //       return response.map(item => {
  //         return new Role(
  //           item.id,
  //           item.name,
  //           item.description,
  //           item.active,
  //           false
  //         );
  //       });
  //     }),
  //     catchError((error) => {
  //       console.log('error caught in service')
  //       console.error(error);

  //       return throwError(error);
  //     })
  //   );
  // }

  // getById(id: number): Observable<Role> {
  //   var url = `${this.baseUrl}/${id.toString()}`;
  //   return this.http.get<Role>(url);
  // }

  // create(role: Role): Observable<Role> {
  //   // create(role: Role) {

  //   var url = `${this.baseUrl}/Create`;

  //   // this.http.post(url, role).subscribe(
  //   //   response => {
  //   //     this.notificationService.showSuccess('Adicionado com sucesso');
  //   //     return true;
  //   //   },
  //   //   error => {
  //   //     this.errorService.handleError('Erro', error);
  //   //     return false;
  //   //   }
  //   // );

  //   this.http.post(url, role).pipe(
  //     map((response: any) => {
  //       return new Role(
  //         response.id,
  //         response.name,
  //         response.description,
  //         response.active,
  //         false
  //       );
  //     }),
  //     catchError((error) => {
  //       console.log('error caught in service')
  //       console.error(error);

  //       return throwError(error);
  //     })
  //   );

  // }

  // delete(roleId: number): void {
  //   var url = `${this.baseUrl}/${roleId}`;

  //   this.http.delete(url).subscribe(role => {
  //     this.notificationService.showSuccess('Deletado com sucesso');
  //   },
  //     error => {
  //       this.errorService.handleError('Erro', error);
  //     });
  // }

}
