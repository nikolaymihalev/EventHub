import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EventValidationConstants } from '../constants/event.validation.constants';
import * as L from 'leaflet';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../../api.service';
import { Category } from '../../types/category';

@Component({
  selector: 'app-add-event',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent implements OnInit{
  private map: L.Map | undefined;

  eventLocation: string = '';
  categoryId: number = 0;
  categories: Category[] = [];

  titleMinLength = EventValidationConstants.TITLE_MIN_LENGTH;
  titleMaxLength = EventValidationConstants.TITLE_MAX_LENGTH;
  descriptionMinLength = EventValidationConstants.DESCRIPTION_MIN_LENGTH;
  descriptionMaxLength = EventValidationConstants.DESCRIPTION_MAX_LENGTH;

  constructor(private http: HttpClient,private apiService: ApiService){}

  ngOnInit(): void {
    this.initMap();
    this.apiService.getCategories().subscribe((categories)=>{
      this.categories = categories;      
    })
  }

  private initMap(): void {
    this.map = L.map('map').setView([42.6977, 23.3219], 13); 

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(this.map);
    
    this.map.on('click', (e: L.LeafletMouseEvent) => {
      const lat = e.latlng.lat;
      const lng = e.latlng.lng;

       this.getAddress(lat, lng).subscribe(
        (address: string) => {
          this.eventLocation = address;                    
        }
      );
    });
  }

  private getAddress(lat: number, lng: number): Observable<string>  {    
    const url = `https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}&addressdetails=1`;

    return this.http.get<any>(url).pipe(
      map((data) => {
        if (data && data.address) {
          const address = data.address;
          const fullAddress = `${address.road || ''}, ${address.city || ''}, ${address.country || ''}`;
          return `${fullAddress}`;
        } else {
          return 'Address not found.';
        }
      }),
      catchError((error) => {
        return new Observable<string>((observer) => {
          observer.next('Failed to retrieve address.');
          observer.complete();
        });
      })
    );
  }
}
