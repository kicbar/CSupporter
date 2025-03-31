import { Component } from '@angular/core';
import { ProductService } from '../services/product.service';
import { Product } from '../models/product.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  product!: Product;
  
  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit() {
    this.productService.productSelected$.subscribe((selectedProduct) => {
      this.product = selectedProduct!;
    });
  }

  navigateToProductAdd() {
    this.router.navigate(['/product-add']);
  }

}
