import { Injectable } from '@angular/core';
import { User } from '../types/user';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, firstValueFrom, map, Observable, tap, throwError } from 'rxjs';

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
          }),
          catchError((err: HttpErrorResponse)=>{      
            return throwError(() => new Error(err.error));
          })          
        ).subscribe();
    }
  }

  async getUserInfo(property: string): Promise<string | undefined> {
    this.getUser();
  
    const userInfo$ = this.user$$.asObservable().pipe(
      map(user => {
        if (user) {
          switch (property) {
            case 'id': return user.id;
            case 'email': return user.email;
            case 'firstName': return user.firstname;
            case 'lastName': return user.lastname;
            case 'username': return user.username;
            default: return undefined;
          }
        }
        return undefined;
      })
    );
  
    return await firstValueFrom(userInfo$);
  }
  
  logout() {
    localStorage.removeItem('authToken');
    this.user$$.next(null);
  }
}
