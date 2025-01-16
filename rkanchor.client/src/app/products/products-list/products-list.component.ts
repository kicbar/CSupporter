import { Component, EventEmitter, Output } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.css'
})
export class ProductsListComponent {
  products!: Product[];

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService.getAllProducts().subscribe(
      (result) => {
          this.products = result;
      },
      (error) => {
          console.error(error);
      }
    );
  }

  onProductSelected(product: Product): void {
    this.productService.selectProduct(product);
  }

}
