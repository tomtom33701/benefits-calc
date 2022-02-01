import { IEmployee } from './employee';
export interface IPersonDto {
  firstName: string;
  lastName: string;
  ssn: string;
}
export interface IEmployeeDto extends IPersonDto {
  employeeId?: number;
  dependents: IPersonDto[];
}
export class EmployeeDto implements IEmployeeDto {

  private constructor(
    public firstName: string,
    public lastName: string,
    public ssn: string,
    public dependents: IPersonDto[],
    public employeeId?: number,
  ) {}
  public static fromEntity(employeeEntity: IEmployee): EmployeeDto {
    const {firstName, lastName, ssn, dependents, employeeId} = employeeEntity;
    const dependentsDto: IPersonDto[] = dependents!.map(p => ({
      firstName: p.firstName ?? '',
      lastName: p.lastName ?? '',
      ssn: p.ssn?.fullSsn ?? ''
    })) ?? [];
    return new EmployeeDto(
      firstName!,
      lastName!,
      ssn!.fullSsn,
      dependentsDto,
      employeeId
      );
  }
}
