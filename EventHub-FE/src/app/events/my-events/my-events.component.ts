import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { EventPageModel } from '../../types/eventsPageModel';
import { SlicePipe } from "../../shared/pipes/slice.pipe";
import { LoaderComponent } from '../../shared/loader/loader.component';
import { ApiService } from '../../api.service';
import { UserService } from '../../user/user.service';
import { RouterLink } from '@angular/router';
import { NotificationService } from '../../shared/notification/notification.service';
import { NotificationComponent } from '../../shared/notification/notification.component';

@Component({
  selector: 'app-my-events',
  standalone: true,
  imports: [SlicePipe, LoaderComponent, RouterLink, NotificationComponent],
  templateUrl: './my-events.component.html',
  styleUrl: './my-events.component.css'
})
export class MyEventsComponent implements OnInit {
  @ViewChild('deleteContainer') deleteContainer!: ElementRef; 
  isDeleteContainerVisible = false;
  currentDeleteTitle = '';
  currentDeleteId = 0;

  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;

  constructor(
    private apiService: ApiService, 
    private userService: UserService, 
    private notificationService: NotificationService, 
    private renderer: Renderer2){}  

  ngOnInit(): void {
    this.getEvents(1);
    this.subscribeToNotification();
  }

  eventsPageModel= {} as EventPageModel;
  pages: number[] = [];
  currentPage: number = 1;
  isLoading = true;
  visiblePages: (number | string)[] = [];


  async changePage(page: number| string):Promise<void>{
    if (typeof page === 'string' || page === this.currentPage) {
      return;
    }
    
    if(typeof page ==='number'){
      await this.getEvents(page);
      this.updateVisiblePages(); 
    }
  }
  
  async getEvents(page: number) : Promise<void>{
    try {
      const userId = await this.userService.getUserPropertyInfo('id');      
      if (userId) {
          this.apiService.getEvents(page,userId).subscribe((eventsPageModel)=>{
            this.setEventModelVariables(eventsPageModel);
        });
      }
    } catch (error) {
      console.error('Error getting user info:', error);
    }    
  }

  async deleteEvent(){
    if(this.currentDeleteId && this.currentDeleteTitle){
      try {
        const userId = await this.userService.getUserPropertyInfo('id');      
        if (userId) {
          this.apiService.deleteEvent(this.currentDeleteId, userId).subscribe({
            next: ()=>{
              this.notificationService.showNotification(`You have successfully deleted ${this.currentDeleteTitle}!`, 'success');  
              this.hasNotification = true;
              this.toggleDeleteContainer();
              this.getEvents(1);
            },
            error: ()=>{
              this.notificationService.showNotification('Operation failed!', 'error');  
              this.hasNotification = true;
            }
          })
        }
      } catch (error) {
        console.error('Error getting user info:', error);
      }    
    }
  }

  toggleDeleteContainer(title?: string, id?: number): void {
    this.isDeleteContainerVisible = !this.isDeleteContainerVisible;
    
    if (this.isDeleteContainerVisible && title && id) {      
      this.renderer.setStyle(this.deleteContainer.nativeElement, 'display', 'block');
      this.currentDeleteTitle = title;
      this.currentDeleteId = id;
    } else {
      this.renderer.setStyle(this.deleteContainer.nativeElement, 'display', 'none');
      this.currentDeleteTitle = '';
      this.currentDeleteId = 0;
    }
  }

  private updateVisiblePages(): void {
    this.visiblePages = [];

    if (this.pages.length <= 5) {
      this.visiblePages = this.pages;
      return;
    }

    this.visiblePages.push(1);

    if (this.currentPage > 3) {
      this.visiblePages.push('...');
    }

    const start = Math.max(2, this.currentPage - 1);
    const end = Math.min(this.pages.length - 1, this.currentPage + 1);
    for (let i = start; i <= end; i++) {
      this.visiblePages.push(i);
    }

    if (this.currentPage < this.pages.length - 2) {
      this.visiblePages.push('...');
    }

    this.visiblePages.push(this.pages.length);
  }

  private getPagesRange(length: number): number[] {
    return Array.from({ length }, (_, i) => i+=1);
  }

  private setEventModelVariables(eventsPageModel: EventPageModel){
    this.eventsPageModel = eventsPageModel;  
    this.pages = this.getPagesRange(eventsPageModel.pagesCount);   
    this.currentPage = eventsPageModel.currentPage;

    this.isLoading = false;

    this.updateVisiblePages();
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
