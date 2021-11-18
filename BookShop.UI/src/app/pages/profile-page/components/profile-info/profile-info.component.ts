import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Address } from 'src/app/common/models/address.model';
import { User } from 'src/app/common/models/user.model';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { UserInfoService } from 'src/app/common/services/user-info.service';


@Component({
  selector: 'profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.scss']
})
export class ProfileInfoComponent implements OnInit, OnDestroy {
  public currentUser: User | null = null;
  public userInfoForm: FormGroup;
  public get addressesValues(): FormGroup[] {
    return this.addresses.controls as FormGroup[];
  }

  private get addresses(): FormArray {
    return this.userInfoForm.get('addresses') as FormArray
  }
  private subscription: Subscription | undefined;

  constructor(private authenticationService: AuthenticationService,
              private userInfoService: UserInfoService) {
    this.userInfoForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl(''),
      age: new FormControl(null),
      addresses: new FormArray([])
    });
  }

  ngOnInit(): void {
    this.subscription = this.authenticationService.currentUser$
      .subscribe(user => this.updateUserInfo(user));
  }

  public onUpdate(): void {
    const updatedUser = new User({
      userId: this.currentUser!.userId,
      email: this.currentUser!.email,
      ...this.userInfoForm.value
    });
    this.userInfoService.updateUser(updatedUser).subscribe();
  }

  public onCancelChanges(): void {
    this.patchForm(this.currentUser);
    this.userInfoForm.markAsPristine();
  }

  public onAddAddress(): void {
    this.addresses.push(this.createAddressFormGroup());
    this.userInfoForm.markAsDirty();
  }

  public onDeleteAddress(position: number): void {
    this.addresses.removeAt(position);
    this.userInfoForm.markAsDirty();
  }

  private updateUserInfo(user: User | null): void {
    this.currentUser = user;
    this.patchForm(user);
  }

  private patchForm(user: User | null): void {
    if (user === null) this.userInfoForm.reset();
    else {
      this.userInfoForm.patchValue({
        name: user!.name,
        surname: user!.surname,
        age: user!.age
      });
      this.addresses.clear();
      this.getAddressesFormGroups(user.addresses)
        .forEach(addressFormGroup => this.addresses.push(addressFormGroup));
      this.userInfoForm.markAsPristine();
    }
  }

  private getAddressesFormGroups(addresses: Address[]): FormGroup[] {
    return addresses.map(a => this.createAddressFormGroup(a));
  }

  private createAddressFormGroup(address?: Address): FormGroup {
    return new FormGroup({
      addressId: new FormControl(address ? address.addressId : -1),
      addressName: new FormControl(address ? address.addressName : '', Validators.required),
      userId: new FormControl(this.currentUser!.userId),
      country: new FormControl(address ? address.country : '', Validators.required),
      district: new FormControl(address ? address.district : ''),
      city: new FormControl(address ? address.city : '', Validators.required),
      street: new FormControl(address ? address.street : '', Validators.required),
      home: new FormControl(address ? address.home : '', Validators.required),
      apartments: new FormControl(address ? address.apartments : '', Validators.required)
    })
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

}
