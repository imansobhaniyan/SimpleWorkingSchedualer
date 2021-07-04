import { Component } from '@angular/core';
import { Router } from '@angular/router';
import LoginResult from 'src/app/models/LoginResult';
import { HttpClientHelper } from 'src/app/utilities/HttpClientHelper';
import LocalStorageHelper from 'src/app/utilities/LocalStorageHelper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  userName: string = "";

  password: string = "";

  isInvalidCredential: boolean = false;

  isLoading: boolean = false;

  router: Router

  httpClient: HttpClientHelper;

  constructor(router: Router, httpCLientHelper: HttpClientHelper) {
    this.router = router;
    this.httpClient = httpCLientHelper;

    if (LocalStorageHelper.hasToken())
      this.router.navigate(['']);
  }

  login(): void {
    this.isLoading = true;
    this.isInvalidCredential = false;
    this.httpClient.post<LoginResult>('api/login', { userName: this.userName, password: this.password }, result => {
      if (!result.success)
        this.isInvalidCredential = true;
      else {
        LocalStorageHelper.setToken(result.token);
        LocalStorageHelper.setRole(result.userRole);
        this.router.navigate(['']);
      }
      this.isLoading = false;
    });
  }

}
