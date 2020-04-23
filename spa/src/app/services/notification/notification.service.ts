import { Injectable } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  positionClass: string = 'toast-bottom-center'

  constructor(private toastr: ToastrService) { }

  showError(message: string, tittle: string = null): void {

    this.toastr.error(message, tittle,
      {
        timeOut: 4000,
        progressBar: true,
        progressAnimation: 'decreasing',
        closeButton: true,
        positionClass: this.positionClass
      });

  }

  showSuccess(message: string, tittle: string = null) {
    this.toastr.success(message, tittle,
      {
        timeOut: 3000,
        progressBar: true,
        progressAnimation: 'decreasing',
        closeButton: true,
        positionClass: this.positionClass
      });
  }

}
