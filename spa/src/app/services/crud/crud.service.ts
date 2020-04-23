import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { delay, tap, take } from 'rxjs/operators';
import { CrudOperations } from 'src/app/interfaces/crud-operations-interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CrudService<T, Id> implements CrudOperations<T, Id> {

  constructor(protected http: HttpClient, @Inject(String) private API_URL: string) { }

  // getAll() {
  //   var url = `${this.API_URL}/GetAll`;
  //   return this.http.get<T[]>(url)
  //     .pipe(
  //       // delay(2000),
  //       tap(console.log)
  //     );
  // }

  save(entity: T) {
    if (entity['id']) {
      return this.update(entity['id'], entity);
    }
    return this.create(entity);
  }

  create(entity: T): Observable<T> {
    return this.http.post<T>(this.API_URL, entity);
  }

  getById(id: Id): Observable<T> {
    return this.http.get<T>(this.API_URL + "/" + id);
  }

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.API_URL)
  }

  update(id: Id, entity: T): Observable<T> {
    return this.http.put<T>(this.API_URL, entity, {});
  }

  delete(id: Id): Observable<T> {
    return this.http.delete<T>(this.API_URL + '/' + id).pipe();
  }

  deleteMany(ids: Id[]): Observable<T> {

    var url = `${this.API_URL}/DeleteMany`;
    var httpParams: HttpParams;

    for (var id of ids) {
      if (!httpParams) {
        httpParams = new HttpParams().set('roleIds', id.toString());
        continue;
      }

      httpParams = httpParams.append('roleIds', id.toString());
    }

    var options = { params: httpParams };

    return this.http.delete<T>(url, options);
  }

}
