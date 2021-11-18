import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from './angular-material.module';
import { CustomMultiselectModule } from './components/custom-multiselect/custom-multiselect.module';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { AddressPipe } from './pipes/address.pipe';
import { MessageTypePipe } from './pipes/message-type.pipe';
import { OrderStatusPipe } from './pipes/order-status.pipe';
import { StartsWithPipe } from './pipes/starts-with.pipe';


@NgModule({
  declarations: [
    LoadingSpinnerComponent,
    StartsWithPipe,
    AddressPipe,
    MessageTypePipe,
    OrderStatusPipe
  ],
  imports: [
    ReactiveFormsModule,
    AngularMaterialModule,
    CustomMultiselectModule
  ],
  exports: [
    ReactiveFormsModule,
    AngularMaterialModule,
    CustomMultiselectModule,
    LoadingSpinnerComponent,
    StartsWithPipe,
    AddressPipe,
    MessageTypePipe,
    OrderStatusPipe
  ]
})
export class SharedModule { }
