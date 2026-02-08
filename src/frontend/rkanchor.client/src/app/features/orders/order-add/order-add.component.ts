import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderService } from '../../../services/order.service';
import { DictionaryService } from '../../../services/dictionary.service';
import { DictionaryType } from '../../../enums/dictionary-type.enum';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-order-add',
  templateUrl: './order-add.component.html',
  styleUrl: './order-add.component.css'
})
export class OrderAddComponent {
  orderForm!: FormGroup;
  producerType!: string[];

  constructor(private fb: FormBuilder, private router: Router, 
    private orderService: OrderService, private dictionaryService: DictionaryService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadProducerTypeDictionary();

    this.orderForm = this.fb.group({
      orderNo: ['', Validators.required],
      orderDate: ['', Validators.required],
      producerType: [null, Validators.required],
      additionalInfo: ['']   
    });
  }

  loadProducerTypeDictionary(): void {
    this.dictionaryService.getDictionary(DictionaryType.Producer).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) 
          this.producerType = response.data;
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
    if (this.orderForm.valid) {
      const order = this.orderForm.value;
      //temporary, add client need to be refactor
      this.orderService.addOrderForClient(order, 1).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.notificationService.customSuccessMessage(`Zamówienie: ${response.data.orderNo}, zostało dodane poprawnie pod identyfikatorem: ${response.data.id}`);
            this.router.navigate(['/orders']);
          } else {
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
          }
        }, 
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas dodawania zamówienia o numerze: ${order.orderNo}, wystąpił błąd!`);
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas dodawania zamówienia: ${order.name} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onAddCancel(): void {
    this.router.navigate(['/orders']);
  }
}
