import { Injectable } from '@angular/core';
import { UserForAuth } from '../types/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private user$$ = new BehaviorSubject<UserForAuth | null>(null);

  get isLogged(): boolean {
    return !!this.user$$.getValue();
  }

  constructor(private http: HttpClient) {
  }

  login(email: string, password: string) {
    return this.http
      .post<{ token: string }>('/api/user/login', { email, password }, { withCredentials: true })
      .pipe(
        tap((response) => {
          if (response.token) {
            sessionStorage.setItem('authToken', response.token);

            this.getUser();
          }
        }),
      );
  }

  getUser() {
    const token = sessionStorage.getItem('authToken');

    if (token) {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

      this.http.get<UserForAuth>('/api/user/getUserInfo', { headers })
        .pipe(
          tap((user) => {
            this.user$$.next(user);
          })
        ).subscribe();
    }
  }

  logout() {
    sessionStorage.removeItem('authToken');
    this.user$$.next(null);
  }
}
