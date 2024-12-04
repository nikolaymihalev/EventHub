import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EventValidationConstants } from '../constants/event.validation.constants';

@Component({
  selector: 'app-add-event',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent {
  titleMinLength = EventValidationConstants.TITLE_MIN_LENGTH;
  titleMaxLength = EventValidationConstants.TITLE_MAX_LENGTH;
  descriptionMinLength = EventValidationConstants.DESCRIPTION_MIN_LENGTH;
  descriptionMaxLength = EventValidationConstants.DESCRIPTION_MAX_LENGTH;
}
