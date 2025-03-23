import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Client } from '../../models/client.model';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-client-add',
  templateUrl: './client-add.component.html',
  styleUrl: './client-add.component.css'
})
export class ClientAddComponent {
  clientForm!: FormGroup;
  clients: Client[] = [];
  
  constructor(private fb: FormBuilder, private clientService: ClientService, private snackBar: MatSnackBar) { }

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
      this.clientService.addClient(client).subscribe(response => {
        if (response.isSuccess && response.data) {
          this.snackBar.open(`Klient ${response.data.firstName} ${response.data.lastName} został dodany poprawnie pod identyfikatorem: ${response.data.id}`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
          });
        } else {
          this.snackBar.open(`Wystąpił błąd podczas dodawania`, 'OK', {
            duration: 3000, 
            horizontalPosition: 'center', 
            verticalPosition: 'top', 
          });
        }
      });
      this.clientForm.reset();
    }
  }


}
