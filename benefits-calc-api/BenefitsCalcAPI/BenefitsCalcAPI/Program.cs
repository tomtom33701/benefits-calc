
var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BenefitsDb");

var services = builder.Services;
services.AddScoped(_ => new SqliteConnection(connString));
//services.AddScoped<IAsyncRepository<E>>()
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR(Assembly.GetExecutingAssembly());
//services.AddValidatorsFromAssemblyContaining()


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

app.MapPost("/employees", async)

app.Run();



async Task EnsureDb(IServiceProvider services, ILogger logger)
{
    logger.LogInformation("Ensuring database exists at connection string '{connectionString}'", connString);
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<SqliteConnection>();
    var createEmployeeSql = $@"CREATE TABLE [Employees] (
                  [EmployeeId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                , [FirstName] text NOT NULL
                , [LastName] text NOT NULL
                , [Ssn] text NOT NULL
                );";
    var createDependentsSql = @"
        CREATE TABLE [Dependents] (
          [Ssn] TEXT NOT NULL
        , [FirstName] TEXT NOT NULL
        , [LastName] TEXT NOT NULL
        , CONSTRAINT [PK_Dependents] PRIMARY KEY ([Ssn])
        );";

    await db.ExecuteAsync(createEmployeeSql);
    await db.ExecuteAsync(createDependentsSql);

}
