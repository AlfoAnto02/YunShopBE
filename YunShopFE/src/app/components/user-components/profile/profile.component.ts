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
  isEditable: boolean = false;

  constructor(private router: Router, private tokenService: TokenService) {}

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    const token = this.tokenService.getToken();
    if (token) {
      const decodedPayload = this.tokenService.decodeToken(token);
      this.user = decodedPayload;

      this.username = this.user.username;
      this.email = this.user.email;

      console.log('User profile loaded:', this.user);
    }
  }

  close(): void {
    this.router.navigateByUrl('');
  }
}