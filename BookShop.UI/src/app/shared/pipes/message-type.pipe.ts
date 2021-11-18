import { Pipe, PipeTransform } from '@angular/core';
import { MessageType } from 'src/app/common/models/message-type.model';

@Pipe({
  name: 'messageType'
})
export class MessageTypePipe implements PipeTransform {
  public transform(value: MessageType | undefined | null): string {
    switch(value) {
      case MessageType.Info:
        return 'Info';
      case MessageType.Warning:
        return 'Warning';
      case MessageType.Ads:
        return 'Advertisement';
      default:
        return 'None';
    }
  }
}
