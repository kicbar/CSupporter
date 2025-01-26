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
          this.snackBar.open(`Produkt o nazwie ${response.name} został dodany poprawnie pod identyfikatorem: ${response.id}`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
          });

          this.router.navigate(['/products']);
        }, 
        error: (err) => {
          this.snackBar.open(`Podczas dodawania produktu o nazwie ${product.name} wystąpił błąd!`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
            panelClass: ['custom-snackbar-error']
          });
          console.log('Bład podczas dodawania prduktu: ' + product.name + ' error: ' + err);
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}
