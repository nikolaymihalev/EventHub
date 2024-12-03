import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, NgForm, NgModel, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmailDirective } from '../../directives/email.directive';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../shared/notification/notification.service';
import { ValidationConstants } from '../constants/validation.constants';
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
  nameMinLength = ValidationConstants.NAME_MIN_LENGTH;
  nameMaxLength = ValidationConstants.NAME_MAX_LENGTH;
  passMinLength = ValidationConstants.PASSWORD_MIN_LENGTH;
  emailMaxLength = ValidationConstants.EMAIL_MAX_LENGTH;
  usernameMinLength = ValidationConstants.USERNAME_MIN_LENGTH;
  usernameMaxLength = ValidationConstants.USERNAME_MAX_LENGTH;

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
        rePassword: new FormControl('', [
          Validators.required
        ]),
      },
      {
        validators: [matchPasswordsValidator('password', 'rePassword')],
      }
    ),
  });

  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;  

  constructor(private userService: UserService, private router: Router, private notificationService: NotificationService) {  }

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
  }
}
