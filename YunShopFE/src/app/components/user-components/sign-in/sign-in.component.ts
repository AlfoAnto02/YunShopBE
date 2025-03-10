import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
  imports: [FormsModule]
})
export class SignInComponent {
  email: string = '';
  password: string = '';

  constructor(private router: Router, private usersService : UserService) {}

  close() {
    this.router.navigateByUrl('');
  }

  switchToSignUp() {
    this.router.navigate(['/SignUp']);
  }

  onSubmit() {
    this.usersService.postLoginUser(this.email, this.password)
      .subscribe({
        next: response => {
          console.log('Login successful:', response.result);
          localStorage.setItem('token', response.result.token);
          console.log('stored token:', localStorage.getItem('token'));
          this.router.navigateByUrl('')
          .then(() => {window.location.reload();});
        },
        error: error => {
          console.error('Error logging in:', error);
          alert('Error logging in: ' + (error.error?.message || 'Unknown error'));
      }
      });
  }
}