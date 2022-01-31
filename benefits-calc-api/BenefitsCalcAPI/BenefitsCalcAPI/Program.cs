using BenefitsCalcAPI.Extensions;
using Domain.Commands;
using Domain.Queries;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BenefitsDb");


var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddScoped(_ => new SqliteConnection(connString));
builder.Services.AddScoped<IDatabase<EmployeeDto>, Database<EmployeeDto>>();
builder.Services.AddScoped<IDatabase<DependentDto>, Database<DependentDto>>();
builder.Services.AddScoped<IAsyncRepository<EmployeeDto>, EmployeeRepository>();
builder.Services.AddScoped<IAsyncRepository<DependentDto>, DependentRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetEmployeesQuery).Assembly);
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var app = builder.Build();

await app.ValidateOrSetUpDatabase(connString);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.MapGet("/error", () => Results.Problem("An error occurred.", statusCode: 500))
    .ExcludeFromDescription();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
//app.UseHttpsRedirection();
app.MapGet("/employess/{id}", async (int id, IMediator mediator) =>
{
    var employee = await mediator.Send(new GetEmployeeByIdQuery { Id = id });
    if (employee is null) Results.NotFound();

    return Results.Ok(EmployeeViewModel.CreateInstance(employee));

}).WithName("GetEmployeeById")
.Produces<EmployeeViewModel>()
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/employees", async (IMediator mediator) =>
{
    var employees = await mediator.Send(new GetEmployeesQuery());
    return employees.Select(EmployeeViewModel.CreateInstance);
}).WithName("GetAllEmployees");

app.MapPost("/employees", async (EmployeeViewModel vm, IMediator mediator, IValidator<EmployeeViewModel> validator) =>
{
    if (!validator.TryValidate(vm, out var validationResult)) return validationResult;

    var entity = await mediator.Send(new CreateEmployeeCommand(vm.ToEntity()));
    var result = EmployeeViewModel.CreateInstance(entity);
    return Results.Created($"/employees/{result.EmployeeId}", result);

})
.WithName("Create Employee")
.ProducesValidationProblem()
.Produces<EmployeeViewModel>(StatusCodes.Status201Created);


app.Run();
