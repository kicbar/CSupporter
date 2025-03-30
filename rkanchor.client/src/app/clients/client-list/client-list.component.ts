import { Component } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrl: './client-list.component.css'
})
export class ClientListComponent {
  clients: { firstName: SafeHtml; lastName: SafeHtml; clientType: SafeHtml }[] = [];

  constructor(private clientService: ClientService, private notficationService: NotificationService, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.refreshClientList();
  }

  refreshClientList(): void {
    this.clientService.getAllClients().subscribe( 
      result => {
      if (result.isSuccess && result.data) {
        this.clients = result.data.map(client => ({
          firstName: this.sanitizer.bypassSecurityTrustHtml(client.firstName),
          lastName: this.sanitizer.bypassSecurityTrustHtml(client.lastName),
          clientType: this.sanitizer.bypassSecurityTrustHtml(client.clientType),
        })
      );
      } else {
        console.log(`Error: ${result.statusCode} - ${result.message}`  );
      }
    });
  }
}
