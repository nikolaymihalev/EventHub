import { Component, OnInit } from '@angular/core';
import { EventPageModel } from '../../types/eventsPageModel';
import { SlicePipe } from "../../shared/pipes/slice.pipe";
import { LoaderComponent } from '../../shared/loader/loader.component';
import { ApiService } from '../../api.service';
import { UserService } from '../../user/user.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-events',
  standalone: true,
  imports: [SlicePipe, LoaderComponent, RouterLink],
  templateUrl: './my-events.component.html',
  styleUrl: './my-events.component.css'
})
export class MyEventsComponent implements OnInit {
  constructor(private apiService: ApiService, private userService: UserService){}  

  ngOnInit(): void {
    this.getEvents(1);
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
      const userId = await this.userService.getUserInfo('id');      
      if (userId) {
          this.apiService.getEvents(page,userId).subscribe((eventsPageModel)=>{
            this.setEventModelVariables(eventsPageModel);
        });
      }
    } catch (error) {
      console.error('Error getting user info:', error);
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
}
