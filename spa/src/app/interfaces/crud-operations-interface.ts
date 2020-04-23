import { Observable } from 'rxjs';

export interface CrudOperations<T, ID> {
    save(entity: T);
    create(t: T): Observable<T>;
    getById(id: ID, t: T);
	getAll(): Observable<T[]>;
	update(id: ID, t: T): Observable<T>;
	delete(id: ID): Observable<any>;
	deleteMany(ids: ID[]): Observable<any>;
}