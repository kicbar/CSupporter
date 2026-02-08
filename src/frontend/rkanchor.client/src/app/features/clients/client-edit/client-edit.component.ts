import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClientService } from '../../../services/client.service';
import { Client } from '../../../models/client.model';
import { Router } from '@angular/router';
import { NotificationService } from '../../../services/notification.service';
import { DictionaryService } from '../../../services/dictionary.service';
import { DictionaryType } from '../../../enums/dictionary-type.enum';

@Component({
  selector: 'app-client-edit',
  templateUrl: './client-edit.component.html',
  styleUrl: './client-edit.component.css'
})
export class ClientEditComponent {
  clientForm!: FormGroup;
  client!: Client;
  clientTypes!: string[];

  constructor(private fb: FormBuilder, private router: Router, 
    private clientService: ClientService, private notificationService: NotificationService, private dictionaryService: DictionaryService) { }

  ngOnInit(): void {
    this.loadClientToEdit();
    this.loadClientTypeDictionary();
  }

  loadClientToEdit(): void {
    this.clientService.clientSelected$.subscribe((selectedClient) => {
      if (selectedClient) {
        this.client = selectedClient;      
          this.clientForm = this.fb.group({
            id: [this.client.id],
            firstName: [this.client.firstName || '', Validators.required],
            lastName: [this.client.lastName || '', Validators.required],
            clientType: [this.client.clientType || null, Validators.required],
            phoneNumber: [this.client.phoneNumber || ''],
            address: [this.client.address || ''],
            email: [this.client.email || '']
          });
      }
    });
  }
  
  loadClientTypeDictionary(): void {
    this.dictionaryService.getDictionary(DictionaryType.Client).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) 
          this.clientTypes = response.data;
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
    if (this.clientForm.valid) {
      const client = this.clientForm.value;   
      this.clientService.editClient(this.client.id, client).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.notificationService.customSuccessMessage(`Klient: ${response.data.firstName} ${response.data.lastName}, został poprawnie edytowany.`);
            this.clientService.selectClient(client);
            this.router.navigate(['/clients']);  
          } else {
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
          }
        }, 
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas edytowania klienta: ${client.firstName} ${client.lastName}, wystąpił błąd!`);
          const status = error?.status ?? '';
          const message = error?.message ?? '';
          console.log(`Błąd podczas edytowania klienta: ${client.firstName} ${client.lastName} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onEditCancel(): void {
    this.router.navigate(['/clients']);
  }

}
