import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) {}

  customSuccessMessage(message: string) {
    this.snackBar.open(message, 'OK', {
      duration: 400000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top', 
      panelClass: ['custom-snackbar-success']
    });
  }

  customErrorMessage(message: string) {
    this.snackBar.open(message, 'OK', {
      duration: 4000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top', 
      panelClass: ['custom-snackbar-error']
    });
  }

  customApiErrorMessage() {
    this.snackBar.open(`Podczas przetwarzania danych wystąpił błąd.`, 'OK', {
      duration: 3000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top', 
    });
  }

  customApiErrorMessageWithLog(statusCode: number, errorMessage: string | undefined) {
    console.log(`Error: ${statusCode} - ${errorMessage}`);
    this.snackBar.open(`Podczas przetwarzania danych wystąpił błąd.`, 'OK', {
      duration: 3000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top', 
    });
  }

}