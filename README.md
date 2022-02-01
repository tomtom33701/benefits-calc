## Employee Benefits Calculator

**Description**
*  This is a code challenge project to create a web interface to allow for a company to set up benefits for employees and their dependents
*  Before the employee's benefits are set up, the user can see a preview of the total benefits cost
*  It also displays a table, listing all employees in the system
*  I was right on the cusp of implementing edit functionality, but I ran out of time
*  I also did not have time to add unit tests

## Architecture
* .NET 6 REST API
  * Uses minimal API framework
  * Includes CQRS pattern with MediatR
  * Database is built with Sqlite
  * Fluent Validations
  * The solution applies Clean Architecture principles
 
 * Front End: Angular 13
   * Uses NgRx Store and Effects
   * Angular Material for UI
   * SCSS in mostly BEM naming convention
   
  
