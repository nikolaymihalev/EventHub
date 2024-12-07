import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../shared/notification/notification.service';
import { NotificationComponent } from "../../shared/notification/notification.component";
import { EmailDirective } from '../../directives/email.directive';
import { UserValidationConstants } from '../constants/user.validation.constants';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NotificationComponent, EmailDirective],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;

  passMinLength = UserValidationConstants.PASSWORD_MIN_LENGTH;

  constructor(private userService: UserService, private router: Router, private notificationService: NotificationService) {}

  ngOnInit() {
    this.checkUserIsLoggedIn();
    this.subscribeToNotification();
  }

  login(form: NgForm) {
    if (form.invalid) {
      return;
    }

    const { email, password } = form.value;

    this.userService.login(email, password).subscribe({
      next: () => {
          this.notificationService.showNotification('Successfully logged in!', 'success');  
          this.hasNotification = true;
          setTimeout(() => {
            this.router.navigate(['/profile']);
          }, 2000);
        },
      error: ()=>{  
        this.notificationService.showNotification('The email or password is invalid!', 'error');  
        this.hasNotification = true;
      }
    });
  }

  private subscribeToNotification(): void{
    this.notificationService.notification$.subscribe(notification => {
      this.notificationMessage = notification.message;
      this.notificationType = notification.type;
      setTimeout(() => {
        this.notificationMessage = '';
        this.hasNotification = false;
      }, 5000);
    });
  }

  private checkUserIsLoggedIn(){
    if(this.userService.isLogged){
      this.router.navigate(['/profile']);
    }
  }
}
