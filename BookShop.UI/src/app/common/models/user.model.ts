import { Address } from "./address.model";

export class User {
    userId: number = -1;
    name: string = '';
    surname: string = '';
    age: number = -1;
    email: string = '';
    addresses: Address[] = [];
    token?: string;

    constructor(init?: Partial<User>) {
        Object.assign(this, init);
    }
}