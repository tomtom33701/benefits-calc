using Domain.DTOs;
using Domain.Repository;

namespace Domain.Commands;

public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly IAsyncRepository<EmployeeDto> _repository;

    public CreateEmployeeCommandHandler(IAsyncRepository<EmployeeDto> repository)
    {
        _repository = repository;
    }
    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = request.Employee;
        var employeeDto = new EmployeeDto(employee.FirstName, employee.LastName, employee.Ssn);

        var savedEmployee = await _repository.SaveAsync(employeeDto);
    }
}