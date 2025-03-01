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
PostLoginUrl = `${environment.baseURL}/api/User/Login`;
PostRegisterUrl = `${environment.baseURL}/api/User/AddUser`;
PutUpdateUserUrl = `${environment.baseURL}/api/User/UpdateUser`;

  constructor(private httpClient : HttpClient, private tokenService: TokenService) { }

  postRegisterUser(username: string, email: string, password: string) {
    return this.httpClient.post(this.PostRegisterUrl, { username, email, password });
  }

  postLoginUser(email: string, password: string): Observable<any> {
    return this.httpClient.post(this.PostLoginUrl, { email, password });
  }

  PutUpdateUser(id: number, addUserRequest: addUserRequest) {
    const updateUserRequest = {
      id: id,
      addUserRequest: addUserRequest
    };

    console.log('Update user request:', updateUserRequest);
    return this.httpClient.put(this.PutUpdateUserUrl, updateUserRequest);
  }
  
  isAuthenticated(): boolean {
    const token = this.tokenService.getToken();
    return !!token && !this.tokenService.isTokenExpired();
  }

  logout() {
    localStorage.removeItem('token');
  }
}
