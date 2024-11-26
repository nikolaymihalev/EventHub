import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';
import { EventPageModel } from '../../types/eventsPageModel';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [],
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css'
})
export class EventsListComponent implements OnInit{
  eventsPageModel= {} as EventPageModel;

  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.apiService.getEvents().subscribe((eventsPageModel)=>{
      this.eventsPageModel = eventsPageModel;      
    })
  }
}
