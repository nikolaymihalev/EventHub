import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../environments/environment.development";
import { EventPageModel } from "./types/eventsPageModel";
import { Category } from "./types/category";

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
        let url = `/api/category/all`;

        return this.http.get<Category[]>(url);
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
}