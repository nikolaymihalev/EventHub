import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../shared/notification/notification.service';
import { UserValidationConstants } from '../constants/user.validation.constants';
import { NotificationComponent } from '../../shared/notification/notification.component';
import { emailValidator } from '../../utils/email.validator';
import { matchPasswordsValidator } from '../../utils/match.passwords.validator';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, NotificationComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  nameMinLength = UserValidationConstants.NAME_MIN_LENGTH;
  nameMaxLength = UserValidationConstants.NAME_MAX_LENGTH;
  passMinLength = UserValidationConstants.PASSWORD_MIN_LENGTH;
  emailMaxLength = UserValidationConstants.EMAIL_MAX_LENGTH;
  usernameMinLength = UserValidationConstants.USERNAME_MIN_LENGTH;
  usernameMaxLength = UserValidationConstants.USERNAME_MAX_LENGTH;

  form = new FormGroup({
    firstName: new FormControl('',[
      Validators.required,
      Validators.minLength(this.nameMinLength),
      Validators.maxLength(this.nameMaxLength),
    ]),
    lastName: new FormControl('',[
      Validators.required,
      Validators.minLength(this.nameMinLength),
      Validators.maxLength(this.nameMaxLength),
    ]),
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(this.usernameMinLength),
      Validators.maxLength(this.usernameMaxLength),
    ]),
    email: new FormControl('', [
      Validators.required, 
      emailValidator()
    ]),
    passGroup: new FormGroup(
      {
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(this.passMinLength),
        ]),
        confirmPassword: new FormControl('', [
          Validators.required
        ]),
      },
      {
        validators: [matchPasswordsValidator('password', 'confirmPassword')],
      }
    ),
  });

  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;  

  constructor(private userService: UserService, private router: Router, private notificationService: NotificationService) {  }

  ngOnInit() {
    this.checkUserIsLoggedIn();
    this.subscribeToNotification();
  }

  isFieldTextMissing(controlName: string) {
    return (
      this.form.get(controlName)?.touched &&
      this.form.get(controlName)?.errors?.['required']
    );
  }

  isNotInMinLength(controlName: string) {
    return (
      this.form.get(controlName)?.touched &&
      this.form.get(controlName)?.errors?.['minlength']
    );
  }

  isOverMaxLength(controlName: string) {
    return (
      this.form.get(controlName)?.touched &&
      this.form.get(controlName)?.errors?.['maxlength']
    );
  }

  get isEmailNotValid() {
    return (
      this.form.get('email')?.touched &&
      this.form.get('email')?.errors?.['emailValidator']
    );
  }

  get passGroup() {
    return this.form.get('passGroup');
  }

  register(){
    if (this.form.invalid) {
      return;
    }

    const {
      firstName,
      lastName,      
      username,
      email,
      passGroup: { password, confirmPassword } = {},
    } = this.form.value;

    if(firstName&&lastName&&username&&email&&password&&confirmPassword){
      this.userService.register(firstName, lastName,username,email,password,confirmPassword).subscribe({
        next: () => {
            this.notificationService.showNotification('You have successfully registered!', 'success');  
            this.hasNotification = true;
            setTimeout(() => {
              this.router.navigate(['/login']);
            }, 2000);
          },
        error: (err: Error)=>{          
          this.notificationService.showNotification(err.message, 'error');  
          this.hasNotification = true;
        }
      });
      }
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
