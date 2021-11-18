import { MessageType } from "./message-type.model";

export class Message {
  messageId: number = -1;
  userId: number = -1;
  type: MessageType = MessageType.None;
  text: string = '';
  date: string = '';
}