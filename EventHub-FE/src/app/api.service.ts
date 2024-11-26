import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../environments/environment.development";
import { Event } from "./types/event";
import { EventPageModel } from "./types/eventsPageModel";
import { Category } from "./types/category";

@Injectable({
    providedIn: 'root',
})
export class ApiService {
    constructor(private http: HttpClient) {}

    getEvents(page?: number, userId?: string){
        const {apiUrl} = environment;

        let url = `${apiUrl}/Event/all`;
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
        const {apiUrl} = environment;
        let url = `${apiUrl}/category/all`;

        return this.http.get<Category[]>(url);
    }
}