import { Component } from '@angular/core';
import { Router } from '@angular/router';
import LocalStorageHelper from 'src/app/utilities/LocalStorageHelper';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  
  isExpanded = false;

  router: Router;

  constructor(router: Router) {
    this.router = router;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut(): void {
    LocalStorageHelper.setToken('');
    this.router.navigate(['login']);
  }
}
