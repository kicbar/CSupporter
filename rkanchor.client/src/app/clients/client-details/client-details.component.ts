import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/client.model';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css'
})
export class ClientDetailsComponent {
  clientRefreshForm: FormGroup;
  foundedClient: Client | undefined;

  constructor(private fb: FormBuilder, private clientService: ClientService, private notificationService: NotificationService) {
    this.clientRefreshForm = this.fb.group({
      lastName: ['', Validators.required]
    });
  }

  onSubmitSearch() {
    if (this.clientRefreshForm.valid) { 
      this.clientService.getClientByLastName(this.clientRefreshForm.value.lastName).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) 
            this.foundedClient = response.data;
          else 
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
        }, 
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas wyszukiwania klietna nazwie: ${this.clientRefreshForm.value.lastName}, wystąpił błąd!`);
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas wyszukiwania klienta: ${this.clientRefreshForm.value.lastName} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }
}
