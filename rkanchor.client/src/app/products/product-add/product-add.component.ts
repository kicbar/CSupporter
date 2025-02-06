import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.css'
})
export class ProductAddComponent {
  productForm!: FormGroup;

  constructor(private fb: FormBuilder, private productService: ProductService, private router: Router, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      productType: ['', Validators.required],
      productCode: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const product = this.productForm.value;
      this.productService.addProduct(product).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.snackBar.open(`Produkt o nazwie ${response.data.name} został dodany poprawnie pod identyfikatorem: ${response.data.id}`, 'OK', {
              duration: 3000, 
              horizontalPosition: 'center', 
              verticalPosition: 'top', 
            });
            this.router.navigate(['/products']);
          }
        }, 
        error: (error) => {
          this.snackBar.open(`Podczas dodawania produktu o nazwie ${product.name} wystąpił błąd!`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
            panelClass: ['custom-snackbar-error']
          });
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas dodawania prduktu: ${product.name} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}
