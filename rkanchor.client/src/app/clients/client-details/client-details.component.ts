import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/client.model';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css'
})
export class ClientDetailsComponent {
  clientForm: FormGroup;
  foundedClient: Client | undefined;

  constructor(private fb: FormBuilder, private clientService: ClientService) {
    this.clientForm = this.fb.group({
      lastName: ['', Validators.required]
    });
  }

  onSubmitSearch() {
    if (this.clientForm.valid) { 
      this.clientService.getClientByLastName(this.clientForm.value.lastName).subscribe((result) => {
        if (result.isSuccess && result.data) {
          this.foundedClient = result.data;
        } else {
          console.log(`Error: ${result.statusCode} - ${result.message}`  );
        }
      });
    }
  }
}
