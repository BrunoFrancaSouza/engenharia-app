import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { SideNavService } from 'src/app/services/menu/sidenav/sidenav.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  menuOptions = ['Menu 1', 'Menu 2', 'Menu 3'];

  constructor(private authService: AuthService,
    private sidenav: SideNavService) { }

  ngOnInit() {
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  hasMenuOptions() {
    return this.menuOptions?.length != 0;
    // return this.menuOptions.some; 
  }

  toggleSidenav() {
    this.sidenav.toggle();
  }

}
