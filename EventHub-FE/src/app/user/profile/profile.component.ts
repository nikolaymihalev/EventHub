import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../../types/user';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserValidationConstants } from '../constants/user.validation.constants';
import { NotificationService } from '../../shared/notification/notification.service';
import { NotificationComponent } from '../../shared/notification/notification.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule, NotificationComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  isEditingMode: boolean = false;
  
  user: User | null = null;

  nameMinLength = UserValidationConstants.NAME_MIN_LENGTH;
  nameMaxLength = UserValidationConstants.NAME_MAX_LENGTH;
  usernameMinLength = UserValidationConstants.USERNAME_MIN_LENGTH;
  usernameMaxLength = UserValidationConstants.USERNAME_MAX_LENGTH;

  notificationMessage: string = '';
  notificationType: string = '';
  hasNotification: boolean = false;

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
    username: new FormControl('',[
      Validators.required,
      Validators.minLength(this.usernameMinLength),
      Validators.maxLength(this.usernameMaxLength),
    ]),
  });


  constructor(private userService: UserService, private notificationService: NotificationService){}

  ngOnInit(): void {
    this.getUserInformation();
    this.subscribeToNotification();
  }

  toggleEditMode(){
    this.isEditingMode = !this.isEditingMode;
  }

  saveUserInformation(){
    if (this.form.valid) {
      const { firstName, lastName, username } = this.form.value;

      const email = this.user?.email;

      this.userService.updateUser(email!, firstName!, lastName!, username!)
        .subscribe({
          next: (response) => {
            this.notificationService.showNotification('User successfully updated!', 'success');  
            this.hasNotification = true;
          setTimeout(() => {
            this.getUserInformation();
            this.toggleEditMode();
          }, 2000);
          },
          error: (err) => {
            this.notificationService.showNotification('Operation failed!', 'error');  
            this.hasNotification = true;
          },
        });
    } else {
      alert('Form is invalid. Please fix the errors and try again.');
    }
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

  private getUserInformation(){
    this.userService.getUser();
    this.userService.user$.subscribe((user)=>{
      this.user = user;
      this.form.patchValue({
        firstName: user?.firstName,
        lastName: user?.lastName,
        username: user?.username,
      })
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
}
