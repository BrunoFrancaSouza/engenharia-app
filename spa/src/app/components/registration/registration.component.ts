import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/Entities/User';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User

  constructor(private authService: AuthService,
    public fb: FormBuilder,
    public router: Router,
    private toastr: ToastrService) {
  }

  ngOnInit() {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      passwords: this.fb.group({
        password: ['', Validators.compose([Validators.required, Validators.minLength(4)])],
        confirmPassword: ['', Validators.required]
      }, { validator: this.comparePasswords })
    });
  }

  comparePasswords(formGroup: FormGroup) {
    const passwordControl = formGroup.get('confirmPassword');

    if (passwordControl.errors == null || passwordControl.hasError('mismatch')) {
      if (formGroup.get('password').value != formGroup.get('confirmPassword').value) {
        passwordControl.setErrors({ mismatch: true });
      }
      else {
        passwordControl.setErrors(null);
      }
    }
  }

  registerUser() {
    if (this.registerForm.valid) {
      this.user = Object.assign(
        { password: this.registerForm.get('passwords.password').value },
        this.registerForm.value);

      // console.log(this.user);

      this.authService.register(this.user).subscribe(
        () => {
          this.toastr.success('Cadastro Realizado');
          this.router.navigate(['/login']);
        },
        error => {
          const erro = error.error;
          erro.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Usuário já cadastrado!');
                break;
                default:
                  this.toastr.error(`Erro no cadastro! CODE: ${element.code}`);
                break;
            }
          });
        }
      );
    }
  }

}
