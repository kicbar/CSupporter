import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) {}

  customGetDataErrorMessage() {
    this.snackBar.open(`Podczas pobierania danych wystąpił błąd.`, 'OK', {
      duration: 3000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top', 
    });
  }

  customGetDataErrorMessageWithLog(statusCode: number, errorMessage: string | undefined) {
    console.log(`Error: ${statusCode} - ${errorMessage}`);
    this.snackBar.open(`Podczas pobierania danych wystąpił błąd.`, 'OK', {
      duration: 3000, 
      horizontalPosition: 'center', 
      verticalPosition: 'top', 
    });
  }
}