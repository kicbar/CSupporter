import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Order } from '../../../models/order.model';
import { OrderService } from '../../../services/order.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css'
})
export class OrdersListComponent {
  orders!: Order[];
  filteredOrders: Order[] = [];
  private refreshOrdersSubscription!: Subscription;

  constructor(private orderService: OrderService, private notificationService: NotificationService) { }

  ngOnInit() {
    this.loadOrders();
    this.refreshOrdersSubscription = this.orderService.refreshOrdersSubject$.subscribe(() => {
      this.loadOrders();
    });
  }

  loadOrders(): void {
    this.orderService.getAllOrders().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.orders = response.data;
          this.filteredOrders = response.data;
        } else {
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
        }
      },
      error: (error) => {
        this.notificationService.customErrorMessage(`Podczas pobierania zamówień, wystąpił błąd!`);
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania listy produktów, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }  

  onOrderSelected(order: Order): void {
    console.log('order selected ' + order.orderNo);
    this.orderService.selectOrder(order);
  }

  onSearch(event: Event): void {
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    
    this.filteredOrders = this.orders.filter((order) => 
      order.orderNo.toLowerCase().includes(searchTerm));
  }

  ngOnDestroy(): void {
    if (this.refreshOrdersSubscription) {
      this.refreshOrdersSubscription.unsubscribe();
    }
  }

  refreshOrderList(): void {
    this.orderService.getAllOrders().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) 
          this.orders = response.data;
        else 
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
      },
      error: (error) => {
        this.notificationService.customErrorMessage(`Podczas pobierania zamówień, wystąpił błąd!`);
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania listy zamówień, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }  
}
