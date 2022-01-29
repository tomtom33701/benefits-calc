import { SsnValueObject } from "./ValueObjects/ssn-value-object";


export interface IPerson {
  firstName?: string;
  lastName?: string;
  ssn?: SsnValueObject
}

export class Person implements IPerson {
  constructor(
    public firstName?: string,
    public lastName?: string,
    public ssn?: SsnValueObject
  ) {}

}
