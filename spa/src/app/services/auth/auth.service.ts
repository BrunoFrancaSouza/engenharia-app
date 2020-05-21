import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators'
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // baseUrl = 'https://localhost:5001/api/auth/';
  baseUrl = environment.apiEndpoint + '/api/auth';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    console.log("environment.apiEndpoint ->", environment.apiEndpoint);
    console.log("baseUrl ->", this.baseUrl);

    return this.http
      .post(`${this.baseUrl}/Login`, model).pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}/Register`, model);
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  getToken() {
    return localStorage.getItem("token");
  }

  facebookLogin(accessToken: string) {
    // let headers = new Headers();
    // headers.append('Content-Type', 'application/json');
    // let body = JSON.stringify({ accessToken });
    // return this.http
    //   .post(this.baseUrl + '/externalauth/facebook', body, { headers })
    //   .map(res => res.json())
    //   .map(res => {
    //     localStorage.setItem('auth_token', res.auth_token);
    //     // this.loggedIn = true;
    //     // this._authNavStatusSource.next(true);
    //     return true;
    //   })
    // .catch(this.handleError);

    return this.http
      .post(`${this.baseUrl}/Login/Facebook`, accessToken).pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
          }
        })
      );
  }

}
