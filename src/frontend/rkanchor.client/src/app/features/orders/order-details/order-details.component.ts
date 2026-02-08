import { Component, Input } from '@angular/core';
import { Order } from '../../../models/order.model';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from '../../../services/notification.service';
import { Router } from '@angular/router';
import { ConfirmationDialogComponent } from '../../../confirmation-dialog/confirmation-dialog.component';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent {
  @Input() order!: Order;

  constructor(private dialog: MatDialog, private router: Router, 
    private orderService: OrderService, private notificationService: NotificationService) { }

  onOrderRemove(order: Order) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {message: `Czy na pewno chcesz usunąć zamówienie: ${order.orderNo}?`}
    });

    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.orderService.removeOrder(order.id).subscribe({
          next: (response) => {
            if (response.isSuccess && response.data) {
              if (response.data === true) 
                this.notificationService.customSuccessMessage(`Zamówienie numer ${order.orderNo} zostało poprawnie usunięte!`);
              else 
                this.notificationService.customErrorMessage(`Podczas usuwania zamówienia o numerze ${order.orderNo} wystąpił błąd!`);
            } else {
              this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
            }
          },
          error: (error) => {
            this.notificationService.customErrorMessage(`Podczas dodawania zamówienia: ${order.orderNo}, wystąpił błąd!`);
            const status =  error?.status ? error.status : '';
            const message =  error?.message ? error.message : '';
            console.log(`Błąd podczas usuwania zamówienia: ${order.orderNo} error: ${error}. Details: ${status}-${message}`);
          }            
        });
      }
    });
  }

  onOrderEdit(order: Order) {
    this.router.navigate(['/order-edit']);
  }

}
