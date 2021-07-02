import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
  }

  login(): void {
    this.isLoading = !this.isLoading;
    this.httpClient.post('/api/login', { userName: this.userName, password: this.password }, result => {
      this.isInvalidCredential = !this.isInvalidCredential;
    });
  }

}
