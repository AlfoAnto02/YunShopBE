import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  LoginUrl = `${environment.baseURL}/User/login`;
  RegisterUrl = `${environment.baseURL}/User/register`;

  constructor(private httpClient : HttpClient, private tokenService: TokenService) { }

  postRegisterUser(username: string, email: string, password: string, phoneNumber: string) {
    return this.httpClient.post(this.RegisterUrl, { username, email, password, phoneNumber });
  }

  postLoginUser(email: string, password: string): Observable<any> {
    return this.httpClient.post(this.LoginUrl, { email, password });
  }
  
  isAuthenticated(): boolean {
    const token = this.tokenService.getToken();
    return !!token && !this.tokenService.isTokenExpired();
  }

  logout() {
    localStorage.removeItem('token');
  }
}
