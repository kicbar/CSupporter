import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../services/notification.service';
import { DictionaryService } from '../../services/dictionary.service';
import { DictionaryType } from '../../enums/dictionary-type.enum';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.css'
})
export class ProductAddComponent {
  productForm!: FormGroup;
  productTypes!: string[];

  constructor(private fb: FormBuilder, private router: Router, 
    private productService: ProductService, private dictionaryService: DictionaryService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadProductTypeDictionary();

    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      productCode: ['', Validators.required],
      productType: [null, Validators.required]
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
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania słówników, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }

  onAddSubmit(): void {
    if (this.productForm.valid) {
      const product = this.productForm.value;
      this.productService.addProduct(product).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.notificationService.customSuccessMessage(`Produkt o nazwie: ${response.data.name}, został dodany poprawnie pod identyfikatorem: ${response.data.id}`);
            this.router.navigate(['/products']);
          } else {
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
          }
        }, 
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas dodawania produktu o nazwie: ${product.name}, wystąpił błąd!`);
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas dodawania produktu: ${product.name} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onAddCancel(): void {
    this.router.navigate(['/products']);
  }
}
