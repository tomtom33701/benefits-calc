namespace Domain.Queries;

public class GetEmployeeByIdQuery: IRequest<Employee>
{
    public int Id { get; set; }
}