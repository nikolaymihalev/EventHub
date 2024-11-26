import { Component, OnInit } from '@angular/core';
import { Event } from '../../types/event';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [],
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css'
})
export class EventsListComponent implements OnInit{
  events: Event[] = [];

  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.apiService.getEvents().subscribe((events)=>{
      this.events = events;            
    })
  }
}
