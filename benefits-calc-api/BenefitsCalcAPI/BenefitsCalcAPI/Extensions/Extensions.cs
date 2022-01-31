

using FluentValidation.Results;

namespace BenefitsCalcAPI.Extensions;

public static class Extensions
{
    public static async Task ValidateOrSetUpDatabase(this WebApplication @this, string connString)
    {

        @this.Logger.LogInformation("Ensuring database exists at connection string '{connectionString}'", connString);
        await using var db = @this.Services.CreateScope().ServiceProvider.GetRequiredService<SqliteConnection>();
        const string createEmployeeSql = 
            @"CREATE TABLE IF NOT EXISTS [Employees] (
              [EmployeeId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
            , [FirstName] text NOT NULL
            , [LastName] text NOT NULL
            , [Ssn] text NOT NULL
            );";
        const string createDependentsSql = 
            @"CREATE TABLE IF NOT EXISTS [Dependents] (
              [Ssn] text NOT NULL
            , [FirstName] text NOT NULL
            , [LastName] text NOT NULL
            , [EmployeeId] bigint NOT NULL
            , CONSTRAINT [PK_Ssn] PRIMARY KEY ([Ssn])
            , CONSTRAINT [FK_Dependents_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE NO ACTION ON UPDATE NO ACTION
            -- , CREATE INDEX [Dependents_IDX_EmployeeId] ON [Dependents] ([EmployeeId] ASC);
            );";

        await db.ExecuteAsync(createEmployeeSql);
        await db.ExecuteAsync(createDependentsSql);

    }
    public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
    {
        return validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
    public static bool TryValidate<TViewModel>(this IValidator<TViewModel> @this, TViewModel viewModel, out IResult result)
    {
        var results = @this.Validate(viewModel);
        result = Results.ValidationProblem(results.ToDictionary());
        return results.IsValid;
    }

}