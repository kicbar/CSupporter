import { Component, Input } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.css'
})
export class ProductEditComponent {
  productForm!: FormGroup;
  product!: Product;

  constructor(private fb: FormBuilder, private productService: ProductService, private router: Router) { }

  ngOnInit(): void {

    this.productService.productSelected$.subscribe((selectedProduct) => {
      if (selectedProduct) {
        this.product = selectedProduct;      
          this.productForm = this.fb.group({
            name: [this.product.name || '', Validators.required],
            description: [this.product.description || '', Validators.required],
            productType: [this.product.productType || '', Validators.required],
            productCode: [this.product.productCode || '', Validators.required]
          });
      }
    });
  }

  onSubmit(): void {

  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }

}
