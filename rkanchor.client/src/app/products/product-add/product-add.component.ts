import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.css'
})
export class ProductAddComponent {
  productForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      productType: ['', Validators.required],
      productCode: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      console.log('Form Submitted:', this.productForm.value);
      alert('Product updated successfully!');
    }
  }
}
