import { Pipe, PipeTransform } from '@angular/core';
import { Address } from 'src/app/common/models/address.model';


@Pipe({
  name: 'address'
})
export class AddressPipe implements PipeTransform {
  public transform(value: Address | null | undefined): string {
    if (value == null) return '';

    const addressSegments: string[] = []
    value.country !== '' && addressSegments.push(value.country);
    value.district !== '' && addressSegments.push(value.district);
    value.city !== '' && addressSegments.push(value.city);
    value.street !== '' && addressSegments.push(value.street);
    value.home !== '' && addressSegments.push(value.home);
    value.apartments !== '' && addressSegments.push(value.apartments);
    return addressSegments.join(', ');
  }
}
