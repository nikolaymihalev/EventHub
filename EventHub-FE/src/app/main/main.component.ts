import { Component } from '@angular/core';
import { EventsListComponent } from '../events/events-list/events-list.component';
import { SearchComponent } from "../events/search/search.component";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [EventsListComponent, SearchComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

}
