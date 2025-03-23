import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResult } from '../models/api.result';
import { environment } from '../../envrinments/environments';
import { UserDto } from '../models/user.dto';
import { MatSnackBar } from '@angular/material/snack-bar';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  logIn(user: UserDto) {
    this.http.post<ApiResult<string>>(`${environment.apiBaseUrl}/User/login`, user).subscribe({
      next: (result) => {
        if (result.isSuccess && result.data) {
          localStorage.setItem(this.tokenKey, result.data);

          this.snackBar.open(`Użytkownik ${user.email} zalogowany`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
          });
        }
        else
          this.snackBar.open(`Nie udało się zalogować`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
          });
      },
      error: (error) => 
      {          
        this.snackBar.open(`Nie udało się zalogować`, 'OK', {
          duration: 3000, 
          horizontalPosition: 'center', 
          verticalPosition: 'top', 
        });
        console.error('Błąd podczas logowania', error);
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
