import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../../types/user';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  isEditingMode: boolean = false;
  
  user: User | null = null;

  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.userService.getUser();
    this.userService.user$.subscribe((user)=>{
      this.user = user;
    });
  }
}
