import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root'
})
@Injectable()
export class SnackBarService {

  constructor(private matSnackBar: MatSnackBar) { }

  public showSuccess(message: string, duration: number = 3000): void {
    this.matSnackBar.open( message, 'OK', {
      duration,
      horizontalPosition: "center",
      panelClass: ['action-info', 'action-success']
    });
  }

  public showError(error: Error, message?: string): void {
    const errorMessage = message || (error.message ? error.message : error.toString());
    this.matSnackBar.open( errorMessage, 'OK', {
      horizontalPosition: "center",
      panelClass: ['action-info', 'action-error']
    });
  }

  public showWarning(warningText: string, duration?: number): void {
    this.matSnackBar.open( warningText, 'OK', {
      duration,
      horizontalPosition: "center",
      panelClass: ['action-info', 'action-warning']
    });
  }

}
