import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'startsWith'
})
export class StartsWithPipe implements PipeTransform {
  public transform(value: string | undefined | null, pattern: string): boolean {
    if (value == null) return false;
    return value.indexOf(pattern) === 0;
  }
}
