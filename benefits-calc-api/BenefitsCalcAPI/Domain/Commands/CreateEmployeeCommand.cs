namespace Domain.Commands;

public record CreateEmployeeCommand(Employee Employee) : IRequest<Employee>; 