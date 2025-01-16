import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.css'
})
export class ProductAddComponent {
  productForm!: FormGroup;

  constructor(private fb: FormBuilder, private productService: ProductService, private router: Router) {}

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
          alert('Produkt dodany poprawnie ' + response.id);
          this.router.navigate(['/products']);
        }, 
        error: (err) => {
          alert('Wystąpił bład podczas dodawania nowego produktu: ' + err);
        }
      });
    }
  }
}
