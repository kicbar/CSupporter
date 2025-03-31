import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrl: './confirmation-dialog.component.css'
})
export class ConfirmationDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string }
  ) {}

  onCancel(): void {
    this.dialogRef.close(false); // Zwraca "false", jeśli użytkownik kliknie "Nie"
  }

  onConfirm(): void {
    this.dialogRef.close(true); // Zwraca "true", jeśli użytkownik kliknie "Tak"
  }
}
