import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClientService } from '../../services/client.service';
import { NotificationService } from '../../services/notification.service';
import { Router } from '@angular/router';
import { DictionaryService } from '../../services/dictionary.service';
import { DictionaryType } from '../../enums/dictionary-type.enum';

@Component({
  selector: 'app-client-add',
  templateUrl: './client-add.component.html',
  styleUrl: './client-add.component.css'
})
export class ClientAddComponent implements OnInit {
  clientForm!: FormGroup;
  clientTypes!: string[];

  constructor(private fb: FormBuilder, private router: Router, 
    private clientService: ClientService, private dictionaryService: DictionaryService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadProductTypeDictionary();

    this.clientForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      clientType: [null, Validators.required]
    });
  }

  loadProductTypeDictionary(): void {
    this.dictionaryService.getDictionary(DictionaryType.Client).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) 
          this.clientTypes = response.data;
        else 
          this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
      },
      error: (error) => {
        this.notificationService.customApiErrorMessage();
        const status =  error?.status ? error.status : '';
        const message =  error?.message ? error.message : '';
        console.log(`Błąd podczas pobierania słówników, error: ${error}. Details: ${status}-${message}`);
      }
    });
  }

  onAddSubmit(): void {
    if (this.clientForm.valid) {
      const client = this.clientForm.value;
      this.clientService.addClient(client).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.notificationService.customSuccessMessage(`Klient: ${response.data.firstName} ${response.data.lastName}, został dodany poprawnie pod identyfikatorem: ${response.data.id}`);
            this.router.navigate(['/clients']);
          } else {
            this.notificationService.customApiErrorMessageWithLog(response.statusCode, response.message);
          }
        }, 
        error: (error) => {
          this.notificationService.customErrorMessage(`Podczas dodawania klienta o nazwie: ${client.firstName} ${client.lastName}, wystąpił błąd!`);
          const status =  error?.status ? error.status : '';
          const message =  error?.message ? error.message : '';
          console.log(`Błąd podczas dodawania klienta: ${client.name} error: ${error}. Details: ${status}-${message}`);
        }
      });
    }
  }

  onAddCancel(): void {
    this.router.navigate(['/clients']);
  }
}
