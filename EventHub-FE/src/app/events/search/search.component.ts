import { Component, OnInit } from '@angular/core';
import { Category } from '../../types/category';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit{
  categories: Category[] = [];

  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.apiService.getCategories().subscribe((categories)=>{
      this.categories = categories;      
    })
  }
}
