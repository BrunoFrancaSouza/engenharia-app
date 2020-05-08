import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  routerOutletComponent: object;
  routerOutletComponentClass: string;

  constructor(
    private authService: AuthService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.checkSessionIsActive();
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  checkSessionIsActive() {
    if (!this.authService.isLoggedIn())
      this.router.navigate(['/login']);;
  }
}

