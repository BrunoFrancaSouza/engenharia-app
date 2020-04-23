import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from "@angular/common/http";
import { Observable, of, throwError } from "rxjs";
import { finalize, catchError, retry } from "rxjs/operators";
import { LoaderService } from '../../services/loader/loader.service';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Injectable()
export class GlobalHttpInterceptorService implements HttpInterceptor {

    constructor(public router: Router,
        public loaderService: LoaderService,
        private notification: NotificationService) { }


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loaderService.show();

        return next.handle(req).pipe(

            finalize(() => this.loaderService.hide()), // Loader Component
            catchError((error) => {

                let handled: boolean = false;
                console.error(error);

                if (error instanceof HttpErrorResponse) {
                    if (error.error instanceof ErrorEvent) {
                        console.error("Error Event");
                    } else {
                        console.log(`error status : ${error.status} ${error.statusText}`);
                        switch (error.status) {
                            case 0:
                                this.notification.showError('Não foi possível se conectar ao servidor');
                                handled = true;
                                break;
                            case 401:      // login
                                // this.router.navigateByUrl("/login");
                                // console.log(`redirect to login`);
                                // handled = true;
                                break;
                            case 403:     // forbidden
                                // this.router.navigateByUrl("/login");
                                // // console.log(`redirect to login`);
                                // handled = true;
                                break;

                            case 408:     // Timeout
                                // console.log(`Time out error`);
                                // handled = true;
                                // retry(1),
                                break;
                        }
                    }
                }
                else {
                    console.error("Other Errors");
                }

                if (handled) {
                    // console.log('return back ');
                    return of(error);
                } else {
                    // console.log('throw error back to to the subscriber');
                    return throwError(error);
                }

            })
        )
    }
}

// Loader Service       -> https://firstclassjs.com/display-a-loader-on-every-http-request-using-interceptor-in-angular-7/
// HTTP Error Handling  -> https://www.tektutorialshub.com/angular/angular-http-error-handling/
