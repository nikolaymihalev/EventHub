import { Injectable } from '@angular/core';
import { UserForAuth } from '../types/user';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private user$$ = new BehaviorSubject<UserForAuth | null>(null);

  get isLogged(): boolean {
    const token = sessionStorage.getItem('authToken');
    return !!token;
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
        catchError((err: HttpErrorResponse)=>{
          return throwError(() => new Error(err.error));
        })
      );
  }

  register(firstName: string, lastName: string, username: string, email: string, password: string, confirmPassword: string){
    return this.http
      .post('/api/user/register', {username, email, firstName, lastName, password, confirmPassword})
      .pipe(
        catchError((err: HttpErrorResponse)=>{
          console.log(err);
          
          return throwError(() => new Error(err.error));
        })
      )
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
