import { Component } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationService } from '../../services/notification.service';
import { DictionaryService } from '../../services/dictionary.service';
import { DictionaryType } from '../../enums/dictionary-type.enum';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.css'
})
export class ProductEditComponent {
  productForm!: FormGroup;
  product!: Product;
  productTypes!: string[];

  constructor(private fb: FormBuilder, private router: Router, 
    private productService: ProductService, private notificationService: NotificationService, private dictionaryService: DictionaryService) { }

  ngOnInit(): void {
    this.loadProductToEdit();
    this.loadProductTypeDictionary();
  }

  loadProductToEdit(): void {
    this.productService.productSelected$.subscribe((selectedProduct) => {
      if (selectedProduct) {
        this.product = selectedProduct;      
          this.productForm = this.fb.group({
            id: [this.product.id],
            name: [this.product.name || '', Validators.required],
            description: [this.product.description || '', Validators.required],
            productType: [this.product.productType || null, Validators.required],
            productCode: [this.product.productCode || '', Validators.required]
          });
      }
    });
  }
  
  loadProductTypeDictionary(): void {
    this.dictionaryService.getDictionary(DictionaryType.Product).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) 
          this.productTypes = response.data;
        else 
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
      },
      error: (error) => {
        this.notificationService.customApiErrorMessage();
        const status = error?.status ?? '';
        const message = error?.message ?? '';
        console.log(`Błąd podczas pobierania słówników, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }

  onEditSubmit(): void {
    if (this.productForm.valid) {
      const product = this.productForm.value;
      this.productService.editProduct(this.product.id, product).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.notificationService.customSuccessMessage(`Produkt o nazwie: ${response.data.name}, został poprawnie edytowany.`);
            this.productService.selectProduct(product);
            this.router.navigate(['/products']);  
          } else {
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
          }
        }, 
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas edytowania produktu o nazwie: ${product.name}, wystąpił błąd!`);
          const status = error?.status ?? '';
          const message = error?.message ?? '';
          console.log(`Błąd podczas edytowania prduktu: ${product.name} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onEditCancel(): void {
    this.router.navigate(['/products']);
  }

}
