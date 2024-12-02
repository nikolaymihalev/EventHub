import { HttpInterceptorFn } from "@angular/common/http";
import { environment } from "../environments/environment.development";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { catchError } from "rxjs";

const { apiUrl } = environment;
const API = '/api';

export const appInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.startsWith(API)) {
    req = req.clone({
      url: req.url.replace(API, apiUrl),
      withCredentials: true,
    });
  }

  const router = inject(Router);

  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401) {
        router.navigate(['/login']);
      }
      return [err];
    })
  );
};