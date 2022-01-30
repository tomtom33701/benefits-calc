var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BenefitsDb");

var services = builder.Services;
var assembly = Assembly.GetExecutingAssembly();
services.AddScoped(_ => new SqliteConnection(connString));
services.AddScoped<IDatabase<EmployeeDto>, Database<EmployeeDto>>();
services.AddScoped<IDatabase<DependentDto>, Database<DependentDto>>();
services.AddScoped<IAsyncRepository<EmployeeDto>, EmployeeRepository>();
services.AddScoped<IAsyncRepository<DependentDto>, DependentRepository>();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR(assembly);
services.AddValidatorsFromAssembly(assembly);


var app = builder.Build();

await EnsureDb(app.Services, app.Logger);

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

//app.UseHttpsRedirection();

//app.MapPost("/employees", async)

app.Run();



async Task EnsureDb(IServiceProvider services, ILogger logger)
{
    logger.LogInformation("Ensuring database exists at connection string '{connectionString}'", connString);
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<SqliteConnection>();
    var createEmployeeSql = $@"CREATE TABLE IF NOT EXISTS [Employees] (
                  [EmployeeId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                , [FirstName] text NOT NULL
                , [LastName] text NOT NULL
                , [Ssn] text NOT NULL
                );";
    var createDependentsSql = @"
        CREATE TABLE IF NOT EXISTS [Dependents] (
          [Ssn] text NOT NULL
        , [FirstName] text NOT NULL
        , [LastName] text NOT NULL
        , [EmployeeId] bigint NOT NULL
        , CONSTRAINT [PK_Ssn] PRIMARY KEY ([Ssn])
        , CONSTRAINT [FK_Dependents_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE NO ACTION ON UPDATE NO ACTION
        );
        CREATE INDEX [Dependents_IDX_EmployeeId] ON [Dependents] ([EmployeeId] ASC);
        ";

    await db.ExecuteAsync(createEmployeeSql);
    await db.ExecuteAsync(createDependentsSql);

}
