import { IPerson } from '@app/benefits/Models/person';
import { SsnValueObject } from "./ValueObjects/ssn-value-object";

/**
 * Represents an individual employee in the system
 *
 * @export
 * @interface IEmployee
 */
export interface IEmployee extends IPerson {
  /**
   * The employee's unique ID for the company
   *
   * @type {number}
   * @memberof IEmployee
   */
  employeeId?: number;
  dependents?: IPerson[];
}

export class Employee implements IEmployee{
  constructor(
   public firstName?: string,
   public lastName?: string,
   public ssn: SsnValueObject = new SsnValueObject(''),
   public employeeId?: number,
   public dependents?: IPerson[]
  ) { }
}
