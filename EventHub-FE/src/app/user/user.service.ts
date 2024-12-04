import { Injectable } from '@angular/core';
import { User } from '../types/user';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private user$$ = new BehaviorSubject<User | null>(null);

  get isLogged(): boolean {
    const token = localStorage.getItem('authToken');
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
            localStorage.setItem('authToken', response.token);

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
          return throwError(() => new Error(err.error));
        })
      )
  }

  getUser() {
    const token = localStorage.getItem('authToken');

    if (token) {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

      this.http.get<User>('/api/user/getUserInfo', { headers })
        .pipe(
          tap((user) => {
            this.user$$.next(user);
          })
        ).subscribe();
    }
  }

  getUserInfo(property: string): string | undefined{
    let returningValue: string | undefined;

    this.getUser();

    switch(property){
      case 'id': returningValue = this.user$$.value?.id; break;
      case 'email': returningValue = this.user$$.value?.email; break;
      case 'firstName': returningValue = this.user$$.value?.firstname; break;
      case 'lastName': returningValue = this.user$$.value?.lastname; break;
      case 'username': returningValue = this.user$$.value?.username; break;
    }    

    return returningValue;
  }

  logout() {
    localStorage.removeItem('authToken');
    this.user$$.next(null);
  }
}
