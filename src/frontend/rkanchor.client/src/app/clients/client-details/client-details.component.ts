import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/client.model';
import { NotificationService } from '../../services/notification.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css'
})
export class ClientDetailsComponent {
  @Input() client!: Client;
 
  constructor(private dialog: MatDialog, private router: Router,
    private clientService: ClientService, private notificationService: NotificationService) { }

  onClientRemove(client: Client) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {message: `Czy na pewno chcesz usunąć klienta: ${client.firstName} ${client.lastName}?`}
    });

    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.clientService.removeClient(client.id).subscribe({
          next: (response) => {
            if (response.isSuccess && response.data) {
              if (response.data === true) 
                this.notificationService.customSuccessMessage(`Klient o identyfikatorze ${client.id} został poprawnie usunięty!`);
              else 
                this.notificationService.customErrorMessage(`Podczas usuwania klienta o identyfikatorze ${client.id} wystąpił błąd!`);
            } else {
              this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
            }
          },
          error: (error) => {
            this.notificationService.customErrorMessage(`Podczas dodawania kleinta: ${client.firstName} ${client.lastName}, wystąpił błąd!`);
            const status =  error?.status ? error.status : '';
            const message =  error?.message ? error.message : '';
            console.log(`Błąd podczas usuwania klienta: ${client.id} error: ${error}. Details: ${status}-${message}`);
          }            
        });
      }
    });
  }

  onClientEdit(client: Client) {
    this.router.navigate(['/client-edit']);
  }
}
