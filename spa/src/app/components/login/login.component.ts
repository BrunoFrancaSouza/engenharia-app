import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { ErrorService } from 'src/app/services/errors/error.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

  @Output() loginEvent: EventEmitter<any> = new EventEmitter();

  data = Object.assign(
    { param1: 'Parâmetro 1' },
    { param2: 'Parâmetro 2' },
  );

  loginForm: FormGroup;

  constructor(private authService: AuthService,
    public router: Router,
    public fb: FormBuilder,
    private errorService: ErrorService) {
  }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigate(['/home']);
      // this.router.navigate(['/roles']);

    this.validation();
  }

  validation() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  login() {
    // this.loginEvent.emit(this.data);

    // if (this.loginForm.hasError) {
    //   this.validation();
    //   return;
    // }

    this.model = Object.assign(this.loginForm.value);

    this.authService.login(this.model).subscribe(
      () => {
        // this.router.navigate(['/home']);
        this.router.navigate(['/roles']);
      },
      error => {
        // this.handleError('Login Error', error);
        this.errorService.handleError('Login Error', error);
      }
    );
  }

  logOut() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  emitAlert() {
    console.log('Alerta emitido via app.component!');
  }
}
