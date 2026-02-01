import { Component } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { NotificationService } from '../../services/notification.service';
import { Client } from '../../models/client.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-clients-list',
  templateUrl: './clients-list.component.html',
  styleUrl: './clients-list.component.css'
})
export class ClientsListComponent {
  clients!: Client[];
  filteredClients: Client[] = [];
  private refreshClientsSubscription!: Subscription;

  constructor(private clientService: ClientService, private notificationService: NotificationService) { }

  ngOnInit() {
    this.loadClients();
    this.refreshClientsSubscription = this.clientService.refreshClientsSubject$.subscribe(() => {
      this.loadClients();
    });
  }

  loadClients(): void {
    this.clientService.getAllClients().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.clients = response.data;
          this.filteredClients = response.data;
        } else {
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
        }
      },
      error: (error) => {
        this.notificationService.customErrorMessage(`Podczas pobierania produktów, wystąpił błąd!`);
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania listy produktów, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }

  onClientSelected(client: Client): void {
    this.clientService.selectClient(client);
  }

  onSearch(event: Event): void {
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    
    this.filteredClients = this.clients.filter((client) => 
      client.firstName.toLowerCase().includes(searchTerm) || 
      client.lastName.toLowerCase().includes(searchTerm)
    );
  }

  ngOnDestroy(): void {
    if (this.refreshClientsSubscription) {
      this.refreshClientsSubscription.unsubscribe();
    }
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
