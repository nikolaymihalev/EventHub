import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';
import { EventPageModel } from '../../types/eventsPageModel';
import { RouterLink } from '@angular/router';
import { Category } from '../../types/category';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css'
})
export class EventsListComponent implements OnInit{
  eventsPageModel= {} as EventPageModel;
  categories: Category[] = [];
  searchTitle: string | any;
  searchCategoryId: number | any;
  pages: number[] = [];
  currentPage: number | any;
  operation: string | any;

  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.apiService.getEvents().subscribe((eventsPageModel)=>{
      this.setEventModelVariables(eventsPageModel);
      this.operation = "all";
    })

    this.apiService.getCategories().subscribe((categories)=>{
      this.categories = categories;      
    })
  }

  search(form: NgForm) {
    if (form.invalid) {
      return;
    }

    this.apiService.searchEvents(this.searchTitle, this.searchCategoryId).subscribe((eventsPageModel) => {
      this.setEventModelVariables(eventsPageModel);
      this.operation = "search";
    });
  }

  changePage(page: number){
    if(this.operation === "all"){
      this.apiService.getEvents(page).subscribe((eventsPageModel)=>{
        this.setEventModelVariables(eventsPageModel);
      })
    }else if(this.operation === "search"){
      this.apiService.searchEvents(this.searchTitle, this.searchCategoryId, page).subscribe((eventsPageModel) => {
        this.setEventModelVariables(eventsPageModel);
      });
    }
  }

  getPagesRange(length: number): number[] {
    return Array.from({ length }, (_, i) => i+=1);
  }

  setEventModelVariables(eventsPageModel: EventPageModel){
    this.eventsPageModel = eventsPageModel;  
    this.pages = this.getPagesRange(eventsPageModel.pagesCount);   
    this.currentPage = eventsPageModel.currentPage;
  }
}
