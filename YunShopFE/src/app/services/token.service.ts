import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      const decoded = atob(payload);
      return JSON.parse(decoded);
    } catch (e) {
      console.error('Invalid token:', e);
      return null;
    }
  }

  getDecodedToken(): any {
    const token = this.getToken();
    return token ? this.decodeToken(token) : null;
  }

  isTokenExpired(): boolean {
    const decodedPayload = this.getDecodedToken();
    if (decodedPayload) {
      const exp = decodedPayload.exp;
      const currentTime = Math.floor(Date.now() / 1000);
      return exp < currentTime;
    }
    localStorage.clear();
    return true;
  }

  getUserIdByToken(): number {
    const decodedPayload = this.getDecodedToken();
    console.log('Decoded payload:', decodedPayload);
    if (!decodedPayload) {
      console.error('Invalid token');
      ;
    }
    return decodedPayload.user_id;
  }

  isUserAdmin(): boolean {
    const decodedPayload = this.getDecodedToken();
    if (!decodedPayload) {
      console.error('Invalid token');
      return false;
    }
    return decodedPayload.role === 'admin' || decodedPayload.role === 'Admin';
  }
}