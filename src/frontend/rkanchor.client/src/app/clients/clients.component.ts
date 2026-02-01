import { Component } from '@angular/core';
import { Client } from '../models/client.model';
import { ClientService } from '../services/client.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.css'
})
export class ClientsComponent {
  client!: Client;
    
  constructor(private clientService: ClientService, private router: Router) { }
  
  ngOnInit() {
    this.clientService.clientSelected$.subscribe((selectedClient) => {
      this.client = selectedClient!;
    });
  }

  navigateToClientAdd() {
    this.router.navigate(['/client-add']);
  }
}
