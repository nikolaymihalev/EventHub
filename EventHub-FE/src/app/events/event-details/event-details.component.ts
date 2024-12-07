import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ApiService } from '../../api.service';
import { Event } from '../../types/event';
import { NotificationService } from '../../shared/notification/notification.service';
import { UserService } from '../../user/user.service';
import { NotificationComponent } from '../../shared/notification/notification.component';

@Component({
  selector: 'app-event-details',
  standalone: true,
  imports: [RouterLink, NotificationComponent],
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.css'
})
export class EventDetailsComponent implements OnInit {
  private userId:string = '';

  currentEvent: Event = { id: 0, title: '', description: '' , categoryId: 0, location: '', date: '', creatorId: '' };
  categoryName: string = '';
  isUserCreator: boolean = false;

  hasNotification: boolean = false;
  notificationMessage: string = '';
  notificationType: string = '';

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private notificationService: NotificationService,
    private router: Router,
    private userService: UserService){}

  ngOnInit(): void {
    const id = this.route.snapshot.params['eventId'];
    this.getEventValues(id);
    this.subscribeToNotification();
  }

  private getEventValues(id: number){
    this.subscribeUserId();
    this.apiService.getEventById(id).subscribe((currentEvent)=>{
      this.currentEvent = currentEvent;

      this.apiService.getCategoryById(currentEvent.categoryId).subscribe((category)=>{
        this.categoryName = category.name;
      });  

      this.isUserCreator = this.userId === currentEvent.creatorId;

      const [day, month, year] = currentEvent.date.split('/');

      this.currentEvent.date =  `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
    });

  }   

  private subscribeUserId(){
    this.userService.getUser();

    this.userService.user$.subscribe((user)=>{   
      this.userId = user?.id!;
    });
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
