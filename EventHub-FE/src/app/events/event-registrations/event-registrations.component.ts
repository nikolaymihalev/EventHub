import { Component, OnInit } from '@angular/core';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { UserService } from '../../user/user.service';
import { RouterLink } from '@angular/router';
import { SlicePipe } from "../../shared/pipes/slice.pipe";
import { NotificationComponent } from '../../shared/notification/notification.component';
import { NotificationService } from '../../shared/notification/notification.service';
import { ApiService } from '../../api.service';
import { Registration } from '../../types/registration';

@Component({
  selector: 'app-event-registrations',
  standalone: true,
  imports: [LoaderComponent, RouterLink, SlicePipe, NotificationComponent],
  templateUrl: './event-registrations.component.html',
  styleUrl: './event-registrations.component.css'
})
export class EventRegistrationsComponent implements OnInit {
  private userId: string = '';
  private id: number | undefined;

  isLoading: boolean = false;
  isDeleteMode: boolean = false;

  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;

  registrations: Registration[] = [];

  constructor(
    private userService:UserService, 
    private notificationService: NotificationService,
    private apiService: ApiService){}

  ngOnInit(): void {
    this.subscribeUserId();
    this.subscribeToNotification();  
    this.getEventRegistrations();  
  }

  toggleDeleteMode(id?: number){
    if(id){
      this.id = id;
    }else {
      this.id = undefined;
    }

    this.isDeleteMode = !this.isDeleteMode;
  }

  unsubscibeFromEvent(){
    if(this.userId && this.id){
      this.apiService.deleteRegistration(this.id, this.userId).subscribe({
        next:()=>{
          this.notificationService.showNotification('Successfully unsubscribed from event!', 'success');  
          this.hasNotification = true;
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
        }
      })
      setTimeout(() => {
        this.getEventRegistrations();
        this.toggleDeleteMode();
      }, 2000);
    }
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

  private subscribeUserId(){
    this.userService.getUser();

    this.userService.user$.subscribe((user)=>{   
      this.userId = user?.id!;
    });
  }

  private getEventRegistrations(){
    if(this.userId){
      this.apiService.getRegistrations(this.userId).subscribe((registrations)=>{
        this.registrations = registrations;
      });
    }
  }
}
