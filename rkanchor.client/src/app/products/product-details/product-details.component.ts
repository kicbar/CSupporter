import { Component, Input } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';
import { Router } from '@angular/router';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent {
  @Input() product!: Product;
 
  constructor(private dialog: MatDialog, private router: Router,
    private productService: ProductService, private notificationService: NotificationService) { }

  onProductRemove(product: Product) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {message: `Czy na pewno chcesz usunąć produkt o nazwie: ${product.name}?`}
    });

    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.productService.removeProduct(product.id).subscribe({
          next: (response) => {
            if (response.isSuccess && response.data) {
              if (response.data === true) {
                this.notificationService.customSuccessMessage(`Produkt o identyfikatorze ${product.id} został poprawnie usunięty!`);
              } else {
                this.notificationService.customErrorMessage(`Podczas usuwania produktu o identyfikatorze ${product.id} wystąpił błąd!`);
              }
            } else {
              this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
            }
          },
          error: (error) => {
            this.notificationService.customErrorMessage(`Podczas dodawania produktu o nazwie: ${product.name}, wystąpił błąd!`);
            const status =  error?.status ? error.status : '';
            const message =  error?.message ? error.message : '';
            console.log(`Błąd podczas usuwania produktu: ${product.id} error: ${error}. Details: ${status}-${message}`);
          }            
        });
      }
    });
  }

  onProductEdit(product: Product) {
    this.router.navigate(['/product-edit']);
  }

}
