import { Observable, throwError } from "rxjs";
import { environment } from "../../envrinments/environments";
import { ApiResult } from "../models/api.result";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Client } from "../models/client.model";
import { ClientDto } from "../models/client.dto";
import { Injectable } from "@angular/core";
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class ClientService {
    private baseUrl = environment.apiBaseUrl;

    constructor(private http: HttpClient, private authService: AuthService) { }

    getAllClients(): Observable<ApiResult<Client[]>> {
      var token = this.authService.getToken();
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });
      
      return this.http.get<ApiResult<Client[]>>(`${this.baseUrl}/Client`, { headers });
    }

    getClientByLastName(lastName: string): Observable<ApiResult<Client>> {
      return this.http.get<ApiResult<Client>>(`${this.baseUrl}/Client/${lastName}`);
    }

    addClient(client: ClientDto): Observable<ApiResult<Client>> {
      var token = this.authService.getToken();
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });

      return this.http.post<ApiResult<Client>>(`${this.baseUrl}/Client`, client, { headers });
    }

    private handleError(error: HttpErrorResponse): Observable<ApiResult<null>> {
        let errorMessage = "Wystąpił nieznany błąd";
    
        if (error.error instanceof ErrorEvent) {
          // Błąd po stronie klienta (np. brak internetu)
          errorMessage = `Błąd klienta: ${error.error.message}`;
        } else {
          // Błąd po stronie serwera
          switch (error.status) {
            case 400:
              errorMessage = "Błąd żądania (400). Sprawdź poprawność danych.";
              break;
            case 401:
              errorMessage = "Brak autoryzacji (401). Zaloguj się ponownie.";
              break;
            case 403:
              errorMessage = "Brak dostępu (403). Nie masz uprawnień.";
              break;
            case 404:
              errorMessage = "Nie znaleziono zasobu (404).";
              break;
            case 500:
              errorMessage = "Błąd serwera (500). Spróbuj ponownie później.";
              break;
            default:
              errorMessage = `Wystąpił błąd: ${error.status} - ${error.message}`;
          }
        }
    
        console.error(errorMessage);
    
        // Zwracamy obiekt ApiResult z informacją o błędzie
        return throwError({
          statusCode: error.status,
          isSuccess: false,
          message: errorMessage,
          data: null,
        });
      }    
}
