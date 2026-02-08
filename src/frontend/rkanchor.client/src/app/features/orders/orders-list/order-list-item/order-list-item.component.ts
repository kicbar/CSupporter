import { Component, Input } from '@angular/core';
import { Order } from '../../../../models/order.model';
import { OrderService } from '../../../../services/order.service';

@Component({
  selector: 'app-order-list-item',
  templateUrl: './order-list-item.component.html',
  styleUrl: './order-list-item.component.css'
})
export class OrderListItemComponent {
  @Input() order!: Order; 
  isActive: boolean = false; 

  constructor(private orderService: OrderService) { }

  ngOnInit() {
    this.orderService.orderSelected$.subscribe((selectedOrder) => {
      this.isActive = this.order === selectedOrder; 
    });
  }
}
