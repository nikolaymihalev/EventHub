import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../../types/user';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserValidationConstants } from '../constants/user.validation.constants';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule],
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


  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.getUserInformation();
  }

  toggleEditMode(){
    this.isEditingMode = !this.isEditingMode;
  }

  saveUserInformation(){
    this.toggleEditMode();
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
}
