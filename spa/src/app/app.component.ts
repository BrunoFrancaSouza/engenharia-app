import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy, ViewChild, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Sistema Engenharia';
  jwtHelper = new JwtHelperService();

  options: any;
  routerOutletComponent: object;
  routerOutletComponentClass: string;

  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;

  menuOptions = ['Menu 1', 'Menu 2', 'Menu 3']
  // menuOptions = []

  sideNavFixedTop = "56";

  constructor(private authService: AuthService,
    public router: Router,
    changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnInit(): void {
    this.checkSessionIsActive();
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  onActivate(component: any) {
    // console.log(component.loginEvent);
    // component.anyFunction();

    // this.routerOutletComponent = component;
    // this.routerOutletComponentClass = component.constructor.name;
    // console.log(this.routerOutletComponentClass);

    //Below will subscribe to the searchItem emitter
    component.loginEvent?.subscribe((data) => {
      // Will receive the data from child here 
      this.options = data;
    })
  };

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  checkSessionIsActive() {
    if (!this.authService.isLoggedIn())
      this.router.navigate(['/login']);;
  }

  hasMenuOptions(){
    return this.menuOptions?.length != 0; 
    // return this.menuOptions.some; 
  }

  setSideNavFixedTop(){
    var toolbar = document.getElementById("toolbar");
    this.sideNavFixedTop = toolbar.style.height;
  }

}

