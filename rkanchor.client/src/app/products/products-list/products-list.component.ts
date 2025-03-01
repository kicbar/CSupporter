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
  filteredProducts: Product[] = [];
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
      next: (result) => {
        // trzeba poprawic tą obsluge
        if (result.isSuccess && result.data) {
          this.products = result.data;
          this.filteredProducts = result.data;
        }
      },
      error: (error) => console.error('Błąd podczas pobierania produktów:', error)
    });
  }

  onProductSelected(product: Product): void {
    this.productService.selectProduct(product);
  }

  onSearch(event: Event): void {
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    
    this.filteredProducts = this.products.filter((product) => 
      product.name.toLowerCase().includes(searchTerm) || 
      product.productCode.toLowerCase().includes(searchTerm)
    );
  }

  ngOnDestroy(): void {
    if (this.refreshProductsSubscription) {
      this.refreshProductsSubscription.unsubscribe();
    }
  }

}
