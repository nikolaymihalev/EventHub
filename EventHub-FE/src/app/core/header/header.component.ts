import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../user/user.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  @ViewChild('profileBtn') profileBtn!: ElementRef; 
  @ViewChild('loggedUserBtns') loggedBtnsContainer!: ElementRef;
  isLoggedBtnsVisible = false;

  get isLoggedIn(): boolean {
    return this.userService.isLogged;
  }

  constructor(
    private userService: UserService, 
    private router: Router,
    private renderer: Renderer2) {}

  logout() {
    this.userService.logout();
    this.router.navigate(['/login']);
  };

  toggleLoggedBtnsContainer(): void {
    if (this.isLoggedBtnsVisible) {
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'display', 'none');
    } else {
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'display', 'flex');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'background', '#F9F7F7');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'padding', '1em');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'margin-top', '17em');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'position', 'absolute');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'border-radius', '0.5em');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'width', '150px');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'gap', '1.5em');
      this.renderer.setStyle(this.loggedBtnsContainer.nativeElement, 'flex-direction', 'column');
    }
    this.isLoggedBtnsVisible = !this.isLoggedBtnsVisible;
  }
}
