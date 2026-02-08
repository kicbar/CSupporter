import { Component, Input } from '@angular/core';
import { Client } from '../../../../models/client.model';
import { ClientService } from '../../../../services/client.service';

@Component({
  selector: 'app-client-list-item',
  templateUrl: './client-list-item.component.html',
  styleUrl: './client-list-item.component.css'
})
export class ClientListItemComponent {
  @Input() client!: Client; 
  isActive: boolean = false; 

  constructor(private clientService: ClientService) { }

  ngOnInit() {
    this.clientService.clientSelected$.subscribe((selectedClient) => {
      this.isActive = this.client === selectedClient; 
    });
  }

}
