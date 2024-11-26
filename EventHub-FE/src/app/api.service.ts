import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../environments/environment.development";
import { Event } from "./types/event";

@Injectable({
    providedIn: 'root',
})
export class ApiService {
    apiUrl = environment;

    constructor(private http: HttpClient) {}

    getEvents(page?: number, userId?: string){
        let url = `${this.apiUrl}/Events/all`;
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

        return this.http.get<Event[]>(url);
    }
}