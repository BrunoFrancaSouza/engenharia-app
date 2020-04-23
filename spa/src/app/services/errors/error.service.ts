import { Injectable } from '@angular/core';
import { NotificationService } from '../notification/notification.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorDetail } from 'src/app/models/Error/ErrorDetail';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private notification: NotificationService) { }

  handleError(tittle: string, error): void {

    if (!error)
      return;

    if (error instanceof HttpErrorResponse)
      return this.handleHttpError(tittle, error);

    var errorMessage = '';

    // Client-side error
    if (error.error && error.error instanceof ErrorEvent)
      // errorMessage = `Error: ${error.error.message}`;
      errorMessage = `${error.error.message}`;

    this.notification.showError(errorMessage, tittle);

  }

  // Server-side errors
  // Common HTTP errors are handled in GlobalHttpInterceptorService class
  handleHttpError(tittle: string, error: HttpErrorResponse) {

    var errorDetail: ErrorDetail = error.error;

    tittle = tittle.concat(` (${error.status})`);

    // if (Array.isArray(error.error)) {
    //   errorMessage = this.getErrorMessageFromArray(error.error);
    // }
    // else {
    //   errorMessage = `${error.error}`;
    // }

    // errorMessage = errorDetail.errorMessage;

    this.notification.showError(errorDetail.ErrorMessage, tittle);

  }

  private getErrorMessageFromArray(errors: any[]): string {
    if (errors.length == 0)
      return null;

    var error = errors[0];
    var errorMessage = '';
    var hasCode: boolean = false
    var hasDescription: boolean = false

    if (error.hasOwnProperty('code')) {
      errorMessage = `Code: ${error.code}`;
      hasCode = true;
    }

    if (error.hasOwnProperty('description')) {
      if (hasCode == true)
        errorMessage = errorMessage.concat(`\n`);

      errorMessage = errorMessage.concat(`Description: ${error.description}`);

      hasDescription = true;
    }

    return errorMessage;

  }

}
