import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
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

  constructor(private fb: FormBuilder, private router: Router, private snackBar: MatSnackBar, 
    private productService: ProductService, private dictionaryService: DictionaryService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.dictionaryService.getDictionary(DictionaryType.Product).subscribe((result) => {
      if (result.isSuccess && result.data) 
        this.productTypes = result.data;
      else 
        this.notificationService.customGetDataErrorMessageWithLog(result.statusCode, result.message);
    });

    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      productCode: ['', Validators.required],
      productType: [null, Validators.required]
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const product = this.productForm.value;
      this.productService.addProduct(product).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.snackBar.open(`Produkt o nazwie ${response.data.name} został dodany poprawnie pod identyfikatorem: ${response.data.id}`, 'OK', {
              duration: 3000, 
              horizontalPosition: 'center', 
              verticalPosition: 'top', 
            });
            this.router.navigate(['/products']);
          }
        }, 
        error: (error) => {
          this.snackBar.open(`Podczas dodawania produktu o nazwie ${product.name} wystąpił błąd!`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
            panelClass: ['custom-snackbar-error']
          });
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas dodawania produktu: ${product.name} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}
