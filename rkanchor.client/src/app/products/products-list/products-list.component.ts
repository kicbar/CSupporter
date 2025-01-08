import { Component } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.css'
})
export class ProductsListComponent {
  products!: Product[];

  constructor(private productService: ProductService, private http: HttpClient) {}

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

}
