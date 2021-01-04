import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {User} from '../models/user';
import {environment} from '../../environments/environment';
import { LoginResponse } from '@app/login/login.model';

const fakeJwtToken = 'fake-jwt-token';
const currentUserKey = 'currentUser';

@Injectable({providedIn: 'root'})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem(currentUserKey)));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) : Observable<LoginResponse> {

    return this.http.post<LoginResponse>(`${environment.authenticationUrl}/login`, {username, password})
      .pipe(map(data => {

        let user = null;
        if (data.success === true) {
          user = new User(username, password, fakeJwtToken);
          localStorage.setItem(currentUserKey, JSON.stringify(user));
          this.currentUserSubject.next(user);
        }
        
        return data;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem(currentUserKey);
    this.currentUserSubject.next(null);
  }
}
