import { Component, Input } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.css'
})
export class ProductEditComponent {
  productForm!: FormGroup;
  product!: Product;

  constructor(private fb: FormBuilder, private productService: ProductService, private router: Router, private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.productService.productSelected$.subscribe((selectedProduct) => {
      if (selectedProduct) {
        this.product = selectedProduct;      
          this.productForm = this.fb.group({
            id: [this.product.id],
            name: [this.product.name || '', Validators.required],
            description: [this.product.description || '', Validators.required],
            productType: [this.product.productType || '', Validators.required],
            productCode: [this.product.productCode || '', Validators.required]
          });
      }
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const product = this.productForm.value;
      this.productService.editProduct(this.product.id, product).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.snackBar.open(`Produkt o nazwie ${response.data.name} został poprawnie edytowany.`, 'OK', {
              duration: 3000, 
              horizontalPosition: 'center', 
              verticalPosition: 'top', 
            });
            this.productService.selectProduct(product);
            this.router.navigate(['/products']);  
          }
        }, 
        error: (error) => {
          this.snackBar.open(`Podczas edytowania produktu o nazwie ${product.name} wystąpił błąd!`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
            panelClass: ['custom-snackbar-error']
          });
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas edytowania prduktu: ${product.name} error: ${error}. Details: ${status}-${message}`);
          
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }

}
