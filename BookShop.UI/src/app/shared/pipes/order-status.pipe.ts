import { Pipe, PipeTransform } from '@angular/core';
import { OrderStatus } from 'src/app/common/models/order-status.model';

@Pipe({
  name: 'orderStatus'
})
export class OrderStatusPipe implements PipeTransform {
  public transform(value: OrderStatus | undefined | null): string {
    switch(value) {
      case OrderStatus.Complete:
        return 'Complete';
      case OrderStatus.DeliveryInProgress:
        return 'Delivery in progress';
      case OrderStatus.ReadyToDelivery:
        return 'Ready to delivery';
      case OrderStatus.Assembling:
        return 'Assembling';
      case OrderStatus.Paid:
        return 'Paid';
      case OrderStatus.UnPaid:
        return 'UnPaid';
      default:
        return 'None';
    }
  }
}
