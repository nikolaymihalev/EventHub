import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';
import { EventPageModel } from '../../types/eventsPageModel';
import { RouterLink } from '@angular/router';
import { Category } from '../../types/category';
import { FormsModule, NgForm } from '@angular/forms';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { SlicePipe } from '../../shared/pipes/slice.pipe';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [RouterLink, FormsModule, LoaderComponent, SlicePipe],
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css'
})
export class EventsListComponent implements OnInit{
  eventsPageModel= {} as EventPageModel;
  pages: number[] = [];
  currentPage: number = 1;
  visiblePages: (number | string)[] = [];
  
  categories: Category[] = [];
  
  searchTitle: string | any;
  searchCategoryId: number | any;
  operation: string | any;
  
  isLoading = true;

  constructor(private apiService: ApiService){
  }

  ngOnInit(): void {
    this.homeEvents();
    this.apiService.getCategories().subscribe((categories)=>{
      this.categories = categories;      
    })
  }

  search(form: NgForm) {
    if (form.invalid) {
      return;
    }
    this.isLoading = true;


    if(this.searchTitle || this.searchCategoryId){
      this.apiService.searchEvents(this.searchTitle, this.searchCategoryId).subscribe((eventsPageModel) => {
        this.setEventModelVariables(eventsPageModel);
        this.operation = "search";
      });
    }else {
      this.homeEvents();
    }
  }

  changePage(page: number| string): void{
    if (typeof page === 'string' || page === this.currentPage) {
      return;
    }
    if(this.operation === "all"){
      this.apiService.getEvents(page).subscribe((eventsPageModel)=>{
        this.setEventModelVariables(eventsPageModel);
      })
    }else if(this.operation === "search"){
      this.apiService.searchEvents(this.searchTitle, this.searchCategoryId, page).subscribe((eventsPageModel) => {
        this.setEventModelVariables(eventsPageModel);
      });
    }
    this.updateVisiblePages(); 
  }

  homeEvents(){
    this.apiService.getEvents().subscribe((eventsPageModel)=>{
      this.setEventModelVariables(eventsPageModel);
      this.operation = "all";
    })
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
