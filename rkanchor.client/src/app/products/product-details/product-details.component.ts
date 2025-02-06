import { Component, Input } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent {
  @Input() product!: Product;
 
  constructor(private productService: ProductService, private snackBar: MatSnackBar, private dialog: MatDialog, private router: Router) {}

  onProductRemove(product: Product) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {message: `Czy na pewno chcesz usunąć produkt o nazwie: ${product.name}?`}
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.productService.removeProduct(product.id).subscribe({
          next: (result) => {
            if (result.isSuccess && result.data) {
              if (result.data === true) {
                this.snackBar.open(`Product poprawnie usunięty!`, 'OK', {
                  duration: 3000, 
                  horizontalPosition: 'center', 
                  verticalPosition: 'top', 
                  panelClass: ['custom-snackbar-error']
                });
              }
            } else {
              this.snackBar.open(`Podczas usuwania produktu wystąpił błąd!`, 'Zamknij', {
                duration: 3000, 
                horizontalPosition: 'center', 
                verticalPosition: 'top', 
                panelClass: ['custom-snackbar-error']
              });
            }
          },
          error: (error) => {
            this.snackBar.open(`Podczas usuwania produktu wystąpił błąd!`, 'Zamknij', {
              duration: 3000, 
              horizontalPosition: 'center', 
              verticalPosition: 'bottom', 
              panelClass: ['custom-snackbar-error']
            });
            console.error('error in onProductRemove: ' + error);
          }
        });
      }
    });
  }

  onProductEdit(product: Product) {
    this.router.navigate(['/product-edit']);
  }
}
