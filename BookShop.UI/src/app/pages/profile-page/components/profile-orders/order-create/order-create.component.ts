import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { OrdersManagementService } from 'src/app/common/services/orders-management.service';
import { RouterParametersService } from 'src/app/common/services/router-parameters.service';
import { User } from 'src/app/common/models/user.model';


@Component({
  selector: 'order-create',
  templateUrl: './order-create.component.html',
  styleUrls: ['./order-create.component.scss']
})
export class OrderCreateComponent implements OnInit, OnDestroy {
  public currentUser: User | null = null;
  public orderInfoFormGroup: FormGroup;

  private subscription?: Subscription;

  constructor(private authenticationService: AuthenticationService,
              private orderManagementService: OrdersManagementService,
              private routerParameterService: RouterParametersService) {
    this.orderInfoFormGroup = new FormGroup({
      addressId: new FormControl(null, Validators.required)
    });
  }

  ngOnInit(): void {
    this.subscription = this.authenticationService.currentUser$
      .subscribe(user => this.currentUser = user);
  }

  public onOrderConfirm(): void {
    const addressId = this.orderInfoFormGroup.value.addressId as number;
    this.orderManagementService.createNewOrder(this.currentUser!.userId, addressId)
      .subscribe(() => this.routerParameterService.updateRoute(['profile/orders/list']));
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

}
