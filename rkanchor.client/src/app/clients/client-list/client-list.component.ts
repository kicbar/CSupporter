import { Component } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { NotificationService } from '../../services/notification.service';
import { ClientDto } from '../../models/client.dto';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrl: './client-list.component.css'
})
export class ClientListComponent {
  clients: ClientDto[] = [];

  constructor(private clientService: ClientService, private notificationService: NotificationService) { }

  ngOnInit() {
    this.refreshClientList();
  }

  refreshClientList(): void {
    this.clientService.getAllClients().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) 
          this.clients = response.data;
        else 
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
      },
      error: (error) => {
        this.notificationService.customErrorMessage(`Podczas pobierania klientów, wystąpił błąd!`);
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania listy klientów, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }
}
