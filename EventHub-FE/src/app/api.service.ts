import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EventPageModel } from "./types/eventsPageModel";
import { Category } from "./types/category";
import { catchError, map, Observable, throwError } from "rxjs";
import { Event } from "./types/event";
import { Comment } from "./types/comment";
import { Registration } from "./types/registration";

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

    getCategoryById(id: number){
        return this.http
            .get<Category>(`/api/category/get-by-id/${id}`)
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                }) 
            );
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

    getEventById(id: number){
        return this.http
            .get<Event>(`/api/event/get-by-id/${id}`)
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                }) 
            );
    }

    editEvent(id: number, title: string, description: string, categoryId: number, date: Date, location: string, userId: string, creatorId: string){
        return this.http
            .put<{message:string}>(`/api/event/update/${userId}`,{id,title, description,date,location,categoryId,creatorId})
            .pipe(
                catchError((err: HttpErrorResponse)=>{                    
                    return throwError(() => new Error(err.error));
                })
            );
    }

    getComments(eventId: number){
        return this.http
            .get<Comment[]>(`/api/comment/get-all/${eventId}`);
    }

    addComment( content: string, userId: string, eventId: number){
        return this.http
            .post<{message:string}>(`/api/comment/add`, {content, userId, eventId})
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }

    deleteComment(id: number, userId: string){
        return this.http
            .delete<{message:string}>(`/api/comment/delete/${id}/user/${userId}`)
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }

    editComment(appUserId: string ,id: number, content: string, userId: string, eventId: number){
        return this.http
            .put<{message:string}>(`/api/comment/update/${appUserId}`, {id, content, userId, eventId})
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }

    getRegistrations(userId: string){
        return this.http
            .get<Registration[]>(`/api/registration/all/${userId}`)
    }

    addRegistration(userId: string, eventId: number,){
        return this.http
            .post<{message:string}>(`/api/registration/add`,{userId, eventId})
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }

    deleteRegistration(id:number, userId:string){
        return this.http
            .delete<{message:string}>(`/api/registration/delete/${id}/user/${userId}`)
            .pipe(
                catchError((err: HttpErrorResponse)=>{
                    return throwError(() => new Error(err.error));
                })
            );
    }
}