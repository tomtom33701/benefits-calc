var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BenefitsDb");


var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddScoped(_ => new SqliteConnection(connString));
builder.Services.AddSingleton<IDatabase>(x => 
    new Database(connString, x.GetRequiredService<ILogger<Database>>()));
builder.Services.AddScoped<IAsyncRepository<EmployeeDto>, EmployeeRepository>();
builder.Services.AddScoped<IAsyncRepository<DependentDto>, DependentRepository>();
var rateCalculators = new IRateCalculator[]
{
    new DependentRateCalculator(),
    new EmployeeRateCalculator()
}.ToImmutableList();
builder.Services.AddSingleton<IImmutableList<IRateCalculator>>(rateCalculators);
builder.Services.AddSingleton<IBenefitCalculationService, BenefitCalculationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetEmployeesQuery).Assembly);
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
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
    app.UseCors("AllowAnyOrigin");
}
else
{
    app.UseHttpsRedirection();
}


app.MapGet("/employees/{id}", async (int id, IMediator mediator) =>
{
    var employee = await mediator.Send(new GetEmployeeByIdQuery { Id = id });
    return employee is null ? Results.NotFound() : Results.Ok(EmployeeViewModel.CreateInstance(employee));

}).WithName("GetEmployeeById")
.Produces<EmployeeViewModel>()
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/employees", async (IMediator mediator) =>
    {
        var employees = await mediator.Send(new GetEmployeesQuery());
        return Results.Ok(employees.Select(EmployeeViewModel.CreateInstance));
    })
    .WithName("GetAllEmployees")
    .Produces<IEnumerable<EmployeeViewModel>>();

app.MapPost("/employees", async (
        EmployeeViewModel vm,
        IMediator mediator,
        IValidator<EmployeeViewModel> validator) =>
{
    if (!validator.TryValidate(vm, out var validationResult)) return validationResult;

    var entity = await mediator.Send(new CreateEmployeeCommand(vm.ToEntity()));
    var result = EmployeeViewModel.CreateInstance(entity);
    return Results.Created($"/employees/{result.EmployeeId}", result);

})
.WithName("Create Employee")
.ProducesValidationProblem()
.Produces<EmployeeViewModel>(StatusCodes.Status201Created);

app.MapPost("/benefits/rates", (
        EmployeeViewModel vm,
        IBenefitCalculationService rateService,
        IValidator<EmployeeViewModel> validator) =>
    {
        if (!validator.TryValidate(vm, out var validationResult)) return validationResult;
        var employee = vm.ToEntity();
        var calculation = rateService.CalculateBenefits(employee);
        var result = BenefitsCalculationViewModel.FromEntity(calculation);
        return Results.Ok(result);
    })
    .WithName("Get Employee Rate")
    .ProducesValidationProblem()
    .Produces<BenefitsCalculationViewModel>(StatusCodes.Status201Created);


app.Run();
