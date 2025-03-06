import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { TokenService } from '../../../services/token.service';
import { User } from '../../../models/user';
import { AppComponent } from '../../../app.component';

@Component({
  selector: 'app-profile',
  imports: [FormsModule, CommonModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user: any = null;
  username: string = '';
  email: string = '';
  password: string = '';
  phone: string = '';
  id: number = 0;

  constructor(private router: Router, private tokenService: TokenService) {}

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    const token = this.tokenService.getToken();
    if (token) {
      const decodedPayload = this.tokenService.decodeToken(token);
      this.user = decodedPayload;

      this.username = this.user.Username;
      this.email = this.user.Email;
      this.id = this.user.Id;
      this.phone = this.user.Phone;

      console.log('User profile loaded:', this.user);
    }
  }

  resetProfile(): void {
    this.user = null;
    this.username = '';
    this.email = '';
    this.password = '';
    this.id = 0;
  }

  close(): void {
    this.router.navigateByUrl('');
  }
}