import { BehaviorSubject, filter, Observable, Subject, tap } from "rxjs";
import { environment } from "../../envrinments/environments";
import { ApiResult } from "../models/api.result";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Client } from "../models/client.model";
import { ClientDto } from "../models/client.dto";
import { Injectable } from "@angular/core";
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private baseUrl = environment.apiBaseUrl;
  public clients: Client[] = [];
  private clientSelectedSubject = new BehaviorSubject<Client | null>(null);
  clientSelected$ = this.clientSelectedSubject.asObservable();
  private refreshClientsSubject = new Subject<void>(); 
  refreshClientsSubject$ = this.refreshClientsSubject.asObservable();
    
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

  editClient(clientId: number, client: ClientDto): Observable<ApiResult<Client>> {
    return this.http.put<ApiResult<Client>>(`${this.baseUrl}/Client/${clientId}`, client);
  }

  removeClient(clientId: number): Observable<ApiResult<boolean>> {
    return this.http.delete<ApiResult<boolean>>(`${this.baseUrl}/Client/${clientId}`).pipe(
      filter((respone) => respone.data === true), 
      tap(() => this.refreshClientsSubject.next())
    );
  }

  selectClient(client: Client): void {
    this.clientSelectedSubject.next(client);
  }  
}
