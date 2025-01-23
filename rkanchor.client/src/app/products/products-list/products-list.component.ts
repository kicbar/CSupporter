import { Component, OnDestroy, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.css'
})
export class ProductsListComponent implements OnInit, OnDestroy {
  products!: Product[];
  private refreshProductsSubscription!: Subscription;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadProducts();
    this.refreshProductsSubscription = this.productService.refreshProductsSubject$.subscribe(() => {
      this.loadProducts();
    });
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe({
      next: (result) => (this.products = result),
      error: (error) => console.error('Błąd podczas pobierania produktów:', error)
    });
  }

  onProductSelected(product: Product): void {
    this.productService.selectProduct(product);
  }

  ngOnDestroy(): void {
    if (this.refreshProductsSubscription) {
      this.refreshProductsSubscription.unsubscribe();
    }
  }

}
