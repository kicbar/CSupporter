import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { environment } from '../../envrinments/environments';
import { Order } from '../models/order.model';
import { BehaviorSubject, filter, Observable, Subject, tap } from 'rxjs';
import { ApiResult } from '../models/api.result';
import { OrderDto } from '../models/order.dto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl = environment.apiBaseUrl;
  public orders: Order[] = [];
  private orderSelectedSubject = new BehaviorSubject<Order | null>(null);
  orderSelected$ = this.orderSelectedSubject.asObservable();
  private refreshOrdersSubject = new Subject<void>(); 
  refreshOrdersSubject$ = this.refreshOrdersSubject.asObservable();
    
  constructor(private http: HttpClient, private authService: AuthService) { }

  getAllOrders(): Observable<ApiResult<Order[]>> {
    var token = this.authService.getToken();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    
    return this.http.get<ApiResult<Order[]>>(`${this.baseUrl}/Order`, { headers });
  }

  addOrderForClient(order: OrderDto, clientId: number): Observable<ApiResult<Order>> {
    var token = this.authService.getToken();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<ApiResult<Order>>(`${this.baseUrl}/Order/client/${clientId}`, order, { headers });
  }  
  
  editOrder(orderId: number, order: OrderDto): Observable<ApiResult<Order>> {
    return this.http.put<ApiResult<Order>>(`${this.baseUrl}/Order/${orderId}`, order);
  }

  removeOrder(orderId: number): Observable<ApiResult<boolean>> {
    return this.http.delete<ApiResult<boolean>>(`${this.baseUrl}/Order/${orderId}`).pipe(
      filter((respone) => respone.data === true), 
      tap(() => this.refreshOrdersSubject.next())
    );
  }

  selectOrder(order: Order): void {
    this.orderSelectedSubject.next(order);
  }    
}
