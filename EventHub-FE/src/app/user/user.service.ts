import { Injectable } from '@angular/core';
import { UserForAuth } from '../types/user';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private user$$ = new BehaviorSubject<UserForAuth | null>(null);
  private user$ = this.user$$.asObservable();

  USER_KEY = '[user]';
  user: UserForAuth | null = null;
  
  get isLogged(): boolean {
    return !!this.user;
  }

  constructor(private http: HttpClient) { }

  login(email: string, password: string) {
    return this.http
      .post<{ token: string }>('/api/user/login', { email, password }, { withCredentials: true })
      .pipe(
        tap((response) => {
          if (response.token) {
            sessionStorage.setItem('authToken', response.token); 
          }
        })
      );
  }

  getUser() {
    return this.user$$.asObservable();
  }

  logout() {
    sessionStorage.removeItem('authToken'); 
    this.user$$.next(null); 
  }
}
