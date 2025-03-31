import { Component, OnDestroy, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.css'
})
export class ProductsListComponent implements OnInit, OnDestroy {
  products!: Product[];
  filteredProducts: Product[] = [];
  private refreshProductsSubscription!: Subscription;

  constructor(private productService: ProductService, private notificationService: NotificationService) { }

  ngOnInit() {
    this.loadProducts();
    this.refreshProductsSubscription = this.productService.refreshProductsSubject$.subscribe(() => {
      this.loadProducts();
    });
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.products = response.data;
          this.filteredProducts = response.data;
        } else {
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
        }
      },
      error: (error) => {
        this.notificationService.customErrorMessage(`Podczas pobierania produktów, wystąpił błąd!`);
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania listy produktów, error: ${error}. Details: ${status}-${message}`);
      }
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
