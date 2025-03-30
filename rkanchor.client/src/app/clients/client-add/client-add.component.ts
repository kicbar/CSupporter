import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Client } from '../../models/client.model';
import { ClientService } from '../../services/client.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-client-add',
  templateUrl: './client-add.component.html',
  styleUrl: './client-add.component.css'
})
export class ClientAddComponent implements OnInit {
  clientForm!: FormGroup;
  clients: Client[] = [];
  
  constructor(private fb: FormBuilder, private clientService: ClientService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.clientForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      clientType: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.clientForm.valid) {
      const client = this.clientForm.value;
      this.clientService.addClient(client).subscribe({
        next: response => {
          if (response.isSuccess && response.data) {
            this.notificationService.customSuccessMessage(`Klient ${response.data.firstName} ${response.data.lastName} został dodany poprawnie pod identyfikatorem: ${response.data.id}`);
          } else {
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
          }
        },
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas dodawania klienta o nazwie: ${client.firstName} ${client.lastName}, wystąpił błąd!`);
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas dodawania klienta: ${client.firstName} ${client.lastName} error: ${error}. Details: ${status}-${message}`);
        }
      });
      this.clientForm.reset();
    }
  }
  
}
