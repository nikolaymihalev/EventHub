import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { EventValidationConstants } from '../constants/event.validation.constants';
import * as L from 'leaflet';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../../api.service';
import { Category } from '../../types/category';
import { NotificationService } from '../../shared/notification/notification.service';
import { Router } from '@angular/router';
import { NotificationComponent } from '../../shared/notification/notification.component';
import { UserService } from '../../user/user.service';
import { parse } from 'date-fns';

@Component({
  selector: 'app-add-event',
  standalone: true,
  imports: [FormsModule, NotificationComponent],
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent implements OnInit{
  private map: L.Map | undefined;

  eventLocation: string = '';
  categoryId: number = 0;
  categories: Category[] = [];
  hasNotification: boolean = false;
  notificationMessage: string = '';
  notificationType: string = '';

  titleMinLength = EventValidationConstants.TITLE_MIN_LENGTH;
  titleMaxLength = EventValidationConstants.TITLE_MAX_LENGTH;
  descriptionMinLength = EventValidationConstants.DESCRIPTION_MIN_LENGTH;
  descriptionMaxLength = EventValidationConstants.DESCRIPTION_MAX_LENGTH;

  constructor(
    private http: HttpClient,
    private apiService: ApiService, 
    private notificationService: NotificationService, 
    private router: Router, 
    private userService: UserService){}

  ngOnInit(): void {
    this.initMap();
    this.subscribeToNotification();
    this.apiService.getCategories().subscribe((categories)=>{
      this.categories = categories;      
    })
  }

  create(form:NgForm){
    if (form.invalid) {
      return;
    }

    const {title, description, category, date, location} = form.value;

    const parsedDate = parse(date, "yyyy-MM-dd", new Date());
    const creatorId = this.userService.getUserInfo('id');
    const categoryID = parseInt(category);

    this.apiService.addEvent(title,description,categoryID,parsedDate,location,creatorId!)
      .subscribe({
        next:()=>{
          this.notificationService.showNotification('Successfully published new event!', 'success');  
          this.hasNotification = true;
          setTimeout(() => {
            this.router.navigate(['/myevents']);
          }, 2000);
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
        }
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

  private subscribeToNotification(): void{
    this.notificationService.notification$.subscribe(notification => {
      this.notificationMessage = notification.message;
      this.notificationType = notification.type;
      setTimeout(() => {
        this.notificationMessage = '';
        this.hasNotification = false;
      }, 5000);
    });
  }
}
