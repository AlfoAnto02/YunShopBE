import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from './token.service';
import { addUserRequest } from '../user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  PostLoginUrl = `${environment.baseURL}/v1/User/login`;
  PostRegisterUrl = `${environment.baseURL}/v1/User/register`;

  constructor(private httpClient : HttpClient, private tokenService: TokenService) { }

  postRegisterUser(username: string, email: string, password: string, phoneNumber: string) {
    return this.httpClient.post(this.PostRegisterUrl, { username, email, password, phoneNumber });
  }

  postLoginUser(email: string, password: string): Observable<any> {
    return this.httpClient.post(this.PostLoginUrl, { email, password });
  }
  
  isAuthenticated(): boolean {
    const token = this.tokenService.getToken();
    return !!token && !this.tokenService.isTokenExpired();
  }

  logout() {
    localStorage.removeItem('token');
  }
}
