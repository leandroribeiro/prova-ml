import { Component } from '@angular/core';
import { Router } from '@angular/router';
// @ts-ignore
import {User} from '@app/models/user';
// @ts-ignore
import {AuthenticationService} from '@app/services/authentication.service';

@Component({ selector: 'app-root', templateUrl: 'app.component.html' })
export class AppComponent {
  currentUser: User;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
