import { Component, Input, HostBinding } from '@angular/core';

@Component({
  selector: 'loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss']
})
export class LoadingSpinnerComponent {
  @Input() set size(value: number) {
    if (!value || value < 14) {
      this._size = 14;
    } else {
      this._size = value;
    }
    const borderSize = Math.floor(0.08 * this._size)
    this._innerSize = Math.floor(0.8 * this._size);
    this._borderSize = borderSize < 2 ? 2 : borderSize;
    this.marginTop = this.marginLeft = this._size / -2;
  };

  @HostBinding('style.marginTop.px') marginTop: number = 0;
  @HostBinding('style.marginLeft.px') marginLeft: number = 0;

  public _size: number = 14;
  public _innerSize: number = 11;
  public _borderSize: number = 2;

  constructor() { }

}
