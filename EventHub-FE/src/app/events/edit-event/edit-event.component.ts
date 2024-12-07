import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { NotificationService } from '../../shared/notification/notification.service';
import { Category } from '../../types/category';
import { EventValidationConstants } from '../constants/event.validation.constants';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../../api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../user/user.service';
import { catchError, map, Observable } from 'rxjs';
import * as L from 'leaflet';
import { NotificationComponent } from '../../shared/notification/notification.component';
import { Event } from '../../types/event';
import { format, parse } from 'date-fns';

@Component({
  selector: 'app-edit-event',
  standalone: true,
  imports: [FormsModule, NotificationComponent],
  templateUrl: './edit-event.component.html',
  styleUrl: './edit-event.component.css'
})
export class EditEventComponent implements OnInit{
  private map: L.Map | undefined;
  private userId:string = '';

  titleMinLength = EventValidationConstants.TITLE_MIN_LENGTH;
  titleMaxLength = EventValidationConstants.TITLE_MAX_LENGTH;
  descriptionMinLength = EventValidationConstants.DESCRIPTION_MIN_LENGTH;
  descriptionMaxLength = EventValidationConstants.DESCRIPTION_MAX_LENGTH;

  categories: Category[] = [];
  hasNotification: boolean = false;
  notificationMessage: string = '';
  notificationType: string = '';

  currentEvent: Event = { id: 0, title: '', description: '' , categoryId: 0, location: '', date: '', creatorId: ''  };
  
  constructor(
    private http: HttpClient,
    private apiService: ApiService, 
    private notificationService: NotificationService, 
    private router: Router, 
    private userService: UserService,
    private route: ActivatedRoute){}

  ngOnInit(): void {
    const id = this.route.snapshot.params['eventId'];

    this.initMap();
    this.subscribeToNotification();
    this.getEventValues(id);
    this.apiService.getCategories().subscribe((categories)=>{
      this.categories = categories;      
    })

  }

  save(form:NgForm){
    if (form.invalid) {
      return;
    }

    const {title, description, category, date, location} = form.value;

    const parsedDate = parse(date, "yyyy-MM-dd", new Date());

    const categoryID = parseInt(category);

    const id = this.route.snapshot.params['eventId'];

    this.editEvent(id, title,description,categoryID, parsedDate, location);

  }

  private editEvent(id: number,title:string,description:string,categoryID:number,parsedDate:Date,location:string) {
    this.userService.getUser();

    this.userService.user$.subscribe((user)=>{   
      this.userId = user?.id!;
    });
    
    this.apiService.editEvent(id,title,description,categoryID,parsedDate,location,this.userId, this.userId)
      .subscribe({
        next:()=>{
          this.notificationService.showNotification(`Successfully saved ${title}!`, 'success');  
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
          this.currentEvent.location = address;                    
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

  private getEventValues(id: number){
    this.apiService.getEventById(id).subscribe((currentEvent)=>{
      this.currentEvent = currentEvent;

      const [day, month, year] = currentEvent.date.split('/');

      this.currentEvent.date =  `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
    }
   );
  }    
}
