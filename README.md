# SAT Recruitment

El objetivo de esta prueba es refactorizar el código de este proyecto.
Se puede realizar cualquier cambio que considere necesario en el código y en los test.


## Requisitos 

- Todos los test deben pasar.
- El código debe seguir los principios de la programación orientada a objetos (SOLID, DRY, etc...).
- El código resultante debe ser mantenible y extensible.

## Refactoring
- The solution implement SOLID using generic repository pattern.
- Contains three principal projects: DataAcces, DomainLogic and API
- The user controll by default use the text file, you can use database too.
- Entity framework Core is used in the Data Access layer but is commented in the controller in order to use the text file.
- BaseResonseModel to serialize data throughs HTTP response and the AutoMapper to map the DataAccess layer model with the domain logic models.
- The API project uses dependency injection to inject concrete classes to interfaces.
- The test project use Moq as mocking library and xUnit as test runner
