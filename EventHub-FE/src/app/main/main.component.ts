import { Component } from '@angular/core';
import { EventsListComponent } from '../events/events-list/events-list.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [EventsListComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

}
