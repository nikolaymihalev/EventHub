import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../shared/notification/notification.service';
import { NotificationComponent } from "../../shared/notification/notification.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NotificationComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;

  constructor(private userService: UserService, private router: Router, private notificationService: NotificationService) {}

  ngOnInit() {
    this.notificationService.notification$.subscribe(notification => {
      this.notificationMessage = notification.message;
      this.notificationType = notification.type;
      setTimeout(() => {
        this.notificationMessage = '';
        this.hasNotification = false;
      }, 5000);
    });
  }

  login(form: NgForm) {
    if (form.invalid) {
      console.error('Invalid Login Form!');
      return;
    }

    const { email, password } = form.value;

    this.userService.login(email, password).subscribe({
      next: () => {
        this.router.navigate(['/profile']);
      },
      error: (err: Error)=>{  
        this.notificationService.showNotification(err.message, 'error');  
        this.hasNotification = true;
      }
    });
  }
}
