import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EventPageModel } from "./types/eventsPageModel";
import { Category } from "./types/category";
import { catchError, throwError } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class ApiService {
    constructor(private http: HttpClient) {}

    getEvents(page?: number, userId?: string){
        let url = `/api/Event/all`;
        if(page){
            url += `?currentPage=${page}`;
            if(userId){
                url+=`&userId=${userId}`;
            }
        }
        else {
            if(userId){
                url+=`?userId=${userId}`;
            }
        }        

        return this.http.get<EventPageModel>(url);
    }

    getCategories(){
        return this.http.get<Category[]>(`/api/category/all`);
    }

    searchEvents(title?: string, categoryId?: number, currentPage?: number){
        let url = `/api/Event/search`;

        if(title && categoryId && currentPage){
            url += `?title=${title}&currentPage=${currentPage}&category=${categoryId}`;
        }else{
            if(title && !categoryId && !currentPage){
                url += `?title=${title}`;
            }else if(!title && categoryId && !currentPage){
                url += `?category=${categoryId}`;
            }else if(title && !categoryId && currentPage){
                url += `?title=${title}&currentPage=${currentPage}`;
            }else if(title && categoryId && !currentPage){
                url += `?title=${title}&category=${categoryId}`;
            }else if(!title && categoryId && currentPage){
                url += `?currentPage=${currentPage}&category=${categoryId}`;
            }            
        } 

        return this.http.get<EventPageModel>(url);
    }

    addEvent(title: string, description: string, categoryId: number, date: Date, location: string, creatorId: string){
        return this.http
            .post<{message:string}>('/api/event/add',{title, description,date,location,categoryId,creatorId})
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }

    deleteEvent(id: number, userId: string){
        return this.http
            .delete<{message: string}>(`/api/event/${id}/user/${userId}`)
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }
}