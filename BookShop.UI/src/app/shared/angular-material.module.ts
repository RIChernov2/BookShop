import { NgModule } from '@angular/core';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';


@NgModule({
    imports: [
        MatButtonModule,
        MatButtonToggleModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatDialogModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        MatListModule,
        MatMenuModule,
        MatRadioModule,
        MatSnackBarModule,
        MatTreeModule
    ],
    exports: [
        MatButtonModule,
        MatButtonToggleModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatDialogModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        MatListModule,
        MatMenuModule,
        MatRadioModule,
        MatSnackBarModule,
        MatTreeModule
    ]
})
export class AngularMaterialModule {}