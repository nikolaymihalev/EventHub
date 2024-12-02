import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit {
  isVisible: boolean | undefined;

  @Input() message: string = '';
  @Input() type: string = 'success';
  @Input() duration: number = 5000;

  ngOnInit() {
    this.showNotification();
  }

  showNotification() {
    this.isVisible = true;
    setTimeout(() => {
      this.isVisible = false;
    }, this.duration);
  }
}
