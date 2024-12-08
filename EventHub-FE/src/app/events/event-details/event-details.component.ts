import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ApiService } from '../../api.service';
import { Event } from '../../types/event';
import { NotificationService } from '../../shared/notification/notification.service';
import { UserService } from '../../user/user.service';
import { NotificationComponent } from '../../shared/notification/notification.component';
import { Comment } from '../../types/comment';
import { User } from '../../types/user';
import { EventValidationConstants } from '../constants/event.validation.constants';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-event-details',
  standalone: true,
  imports: [RouterLink, NotificationComponent, FormsModule],
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.css'
})
export class EventDetailsComponent implements OnInit {
  commentContentMinLength = EventValidationConstants.COMMENT_MIN_CONTENT;
  commentContentMaxLength = EventValidationConstants.COMMENT_MAX_CONTENT;

  userId:string = '';

  currentEvent: Event = { id: 0, title: '', description: '' , categoryId: 0, location: '', date: '', creatorId: '' };
  categoryName: string = '';
  
  isUserCreator: boolean = false;
  eventCreator: User | undefined;
  
  comments: Comment[] = [];
  commentsUsersnames: string[] = [];
  currentComment: Comment | any;

  isAddMode: boolean = false;
  isEditMode: boolean = false;

  hasNotification: boolean = false;
  notificationMessage: string = '';
  notificationType: string = '';

  
  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private notificationService: NotificationService,
    private userService: UserService){}
    
  get isLogged() { 
    return this.userService.isLogged;
  }

  ngOnInit(): void {
    const id = this.route.snapshot.params['eventId'];
    this.getEventValues(id);
    this.subscribeToNotification();
  }

  toggleAddMode(){
    this.isAddMode = !this.isAddMode;
  }

  toggleEditMode(comment?: Comment){
    if(comment){
      this.currentComment = comment;
    }else{
      this.currentComment = undefined;
    }

    this.isEditMode = !this.isEditMode;
  }

  createComment(form:NgForm){
    if(form.invalid)
      return;

    const { content } = form.value;

    const eventId = this.route.snapshot.params['eventId'];
    
    if(this.userId){
      this.apiService.addComment(content, this.userId, eventId).subscribe({
        next:()=>{
          this.notificationService.showNotification('Successfully poster new comment!', 'success');  
          this.hasNotification = true;
          setTimeout(() => {
            this.toggleAddMode();
            this.getComments(eventId);
          }, 2000);
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
          setTimeout(() => {
            this.toggleAddMode();
            this.getComments(eventId);
          }, 2000);
        }
      })
    }
  }

  updateComment(form:NgForm){
    if(form.invalid)
      return;

    const { content } = form.value;

    const eventId = this.route.snapshot.params['eventId'];
     
    if(this.userId){
      this.apiService.editComment(this.userId, this.currentComment.id, content, this.currentComment.userId, eventId).subscribe({
        next:()=>{
          this.notificationService.showNotification('Successfully updated your comment!', 'success');  
          this.hasNotification = true;
          setTimeout(() => {
            this.toggleEditMode();
            this.getComments(eventId);
          }, 2000);
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
          setTimeout(() => {
            this.toggleEditMode();
            this.getComments(eventId);
          }, 2000);
        }
      })
    }
  }

  private getEventValues(id: number){
    this.subscribeUserId();
    this.apiService.getEventById(id).subscribe((currentEvent)=>{
      this.currentEvent = currentEvent;

      this.userService.getInformation(currentEvent.creatorId).subscribe((user)=>{
        this.eventCreator = user;
      })

      this.apiService.getCategoryById(currentEvent.categoryId).subscribe((category)=>{
        this.categoryName = category.name;
      });  

      this.isUserCreator = this.userId === currentEvent.creatorId;
      
      const [day, month, year] = currentEvent.date.split('/');
      
      this.currentEvent.date =  `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
    });
    
    this.getComments(id);    
  }   

  private getComments(id: number){
    this.apiService.getComments(id).subscribe((comments)=>{
      this.comments = comments;   
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
