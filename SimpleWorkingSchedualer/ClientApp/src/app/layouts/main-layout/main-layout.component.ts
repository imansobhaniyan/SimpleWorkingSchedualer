import { Component } from '@angular/core';
import { Router } from '@angular/router';
import LocalStorageHelper from 'src/app/utilities/LocalStorageHelper';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html'
})
export class MainLayoutComponent {

  constructor(router: Router) {
    if (!LocalStorageHelper.hasToken())
      router.navigate(['login']);
  }

}
