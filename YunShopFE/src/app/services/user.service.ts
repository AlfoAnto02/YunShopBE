import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  LoginUrl = `${environment.baseURL}/Users/login`;
  RegisterUrl = `${environment.baseURL}/Users/register`;
  GetUserByIdUrl = `${environment.baseURL}/Users`;

  constructor(private httpClient : HttpClient, private tokenService: TokenService) { }

  postRegisterUser(username: string, email: string, password: string, phoneNumber: string) {
    return this.httpClient.post(this.RegisterUrl, { username, email, password, phoneNumber });
  }

  postLoginUser(email: string, password: string): Observable<any> {
    return this.httpClient.post(this.LoginUrl, { email, password });
  }

  getUserById(id: number): Observable<any> {
    return this.httpClient.get(`${this.GetUserByIdUrl}/${id}`);
  }
  
  isAuthenticated(): boolean {
    const token = this.tokenService.getToken();
    return !!token && !this.tokenService.isTokenExpired();
  }

  logout() {
    localStorage.clear();
  }
}
