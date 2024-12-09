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
import { forkJoin, map, switchMap } from 'rxjs';

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
  isJoinedToEvent: boolean = false;
  
  comments: Comment[] = [];
  currentComment: Comment | any;

  isAddMode: boolean = false;
  isEditMode: boolean = false;
  showMode: boolean = false;
  isDeleteMode: boolean = false;

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
    this.checkIfUserIsJoinedToEvent(id);
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

  showComments(){
    this.toggleShowMode();
    this.getComments();
  }

  toggleShowMode(){
    this.showMode = !this.showMode;
    this.comments = [];
  }

  toggleDeleteMode(comment?: Comment){
    if(comment){
      this.currentComment = comment;
    }else{
      this.currentComment = undefined;
    }

    this.isDeleteMode = !this.isDeleteMode;
  }

  deleteComment(){
    if(this.userId){
      this.apiService.deleteComment(this.currentComment.id, this.userId).subscribe({
        next:()=>{
          this.notificationService.showNotification('Successfully deleted your comment!', 'success');  
          this.hasNotification = true;
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
        }
      })
      setTimeout(() => {
        this.getComments();
        this.toggleDeleteMode();
      }, 2000);
    }
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
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
        }
      })
      setTimeout(() => {
        this.getComments();
        this.toggleAddMode();
      }, 2000);
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
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
        }
      })
      setTimeout(() => {
        this.getComments();
        this.toggleEditMode();
      }, 2000);
    }
  }

  joinToEvent(){
    if(this.userId && this.isJoinedToEvent === false){
      const eventId = this.route.snapshot.params['eventId'];
      this.apiService.addRegistration(this.userId, eventId).subscribe({
        next:()=>{
          this.notificationService.showNotification('Successfully joined to event!', 'success');  
          this.hasNotification = true;
          this.isJoinedToEvent = true;
        },
        error: ()=>{  
          this.notificationService.showNotification('Operation failed. Try again!', 'error');  
          this.hasNotification = true;
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
  }   

  private getComments(){
    const id = this.route.snapshot.params['eventId'];

    this.apiService.getComments(id).pipe(
      switchMap((comments: Comment[]) => {
        const commentObservables = comments.map((comment) =>
          this.userService.getInformation(comment.userId).pipe(
            map((user) => ({
              ...comment,
              username: user.username,
            }))
          )
        );

        return forkJoin(commentObservables);
      })
    ).subscribe((commentsWithUsername) => {
      this.comments = commentsWithUsername;
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

  private checkIfUserIsJoinedToEvent(id: number){
    this.apiService.getRegistrations(this.userId).subscribe((registrations) => {
      this.isJoinedToEvent = registrations.some(registration => registration.eventId == id);      
    });
  }
}
