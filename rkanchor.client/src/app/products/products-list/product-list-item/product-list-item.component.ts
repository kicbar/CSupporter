import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../../../models/product.model';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-product-list-item',
  templateUrl: './product-list-item.component.html',
  styleUrl: './product-list-item.component.css'
})
export class ProductListItemComponent implements OnInit {
  @Input() product!: Product; 
  isActive: boolean = false; 

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.productSelected.subscribe((selectedProduct) => {
      this.isActive = this.product === selectedProduct; 
    });
  }

  onSelected(event: Event) {
    event.preventDefault(); 
    this.productService.selectProduct(this.product); 
  }

}
