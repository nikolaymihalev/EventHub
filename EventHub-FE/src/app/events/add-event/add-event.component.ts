import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { EventValidationConstants } from '../constants/event.validation.constants';
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
  private userId: string = '';

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
    private apiService: ApiService, 
    private notificationService: NotificationService, 
    private router: Router, 
    private userService: UserService){}

  ngOnInit(): void {
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
    
    const categoryID = parseInt(category);
    
    this.addEvent(title,description,categoryID, parsedDate, location);
    
  }
  
  private addEvent(title:string,description:string,categoryID:number,parsedDate:Date,location:string) {
    this.userService.getUser();
    this.userService.user$.subscribe((user)=>{  
       this.userId = user?.id!;
    });
    this.apiService.addEvent(title,description,categoryID,parsedDate,location,this.userId)
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
