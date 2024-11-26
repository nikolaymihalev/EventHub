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

  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.apiService.getEvents().subscribe((eventsPageModel)=>{
      this.eventsPageModel = eventsPageModel;      
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
      this.eventsPageModel = eventsPageModel;
    });
  }
}
