import { Component } from '@angular/core';
import { Order } from '../models/order.model';
import { Router } from '@angular/router';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
  order!: Order;

  constructor(private orderService: OrderService, private router: Router) { }

  ngOnInit() {
    this.orderService.orderSelected$.subscribe((selectedOrder) => {
      this.order = selectedOrder!;
    });
  }

  navigateToOrderAdd() {
    this.router.navigate(['/order-add']);
  }
}
