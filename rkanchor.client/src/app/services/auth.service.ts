import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResult } from '../models/api.result';
import { environment } from '../../envrinments/environments';
import { UserDto } from '../models/user.dto';
import { jwtDecode } from 'jwt-decode';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient, private notificationService: NotificationService) { }

  logIn(user: UserDto) {
    this.http.post<ApiResult<string>>(`${environment.apiBaseUrl}/User/login`, user).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          localStorage.setItem(this.tokenKey, response.data);
          this.notificationService.customSuccessMessage(`Użytkownik ${user.email} zalogowany`);
        } else {
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
        }
      },
      error: (error) => 
      {          
        this.notificationService.customErrorMessage(`Podczas logowania użytwkonika ${user.email} wystąpił błąd!`);
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas dodawania logowania: ${user.email} error: ${error}. Details: ${status}-${message}`);
      }
    });
  }

  logOut() {
    localStorage.removeItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    return this.isTokenActive();
  }

  isTokenActive(): boolean {
    const token = localStorage.getItem(this.tokenKey);;
    if (!token) 
      return false; 

    try {
      const decoded: any = jwtDecode(token); 
      const exp = decoded.exp; 
      const now = Math.floor(Date.now() / 1000); 

      return exp > now; 
    } catch (error) {
      return false;
    }
  }  

  getToken(): string {
    const token = localStorage.getItem(this.tokenKey);
    return token ?? '';
  }
}
