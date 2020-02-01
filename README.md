# How to run Payment Gateway

#### Payment Gateway Set-up
The target framework is netcoreapp3.0. To run the solution,  download and install .NET Core SDK v3.0.0 or later at https://dotnet.microsoft.com/download/dotnet-core/3.0

PowerShell is required to run a build script which compiles the solution, migrate the database, and run tests.

1. Clone or download the solution.
2. Open a PowerShell with admin privilege. Go to the solution root folder and execute `./build.ps1`. This complies the solution, migrates database and run tests.
4. Open a terminal (or command) window and change directory to Api root folder. Execute `dotnet run` command.
5. Change directory to IdentityServer and execute `dotnet run` command.
5. Visit https://localhost:5001.

Note: If run into a permission error like "Can't write to SQLite ...", make sure the terminal is run as Admin privilege.

####Give it a go
Use sample request 

# Dependency rules in the architecture
The model of the solution is based on Clean Architecture and CQRS. 
The concentric circles in [Figure 1] represent different areas of software. The inner circles are policies; the outer circles are mechanism. The further in you go, the higher level the software becomes, in general.

[Figure 1] - Arrows across layers represent dependency flow

![dependency flow](Documents/dependency-flow.png)

*Source code dependencies must point only inward, toward higher-level policies.*

By separating the software into layers and conforming to the dependency rules, a system becomes intrinsically testable.


- Domain contains enterprise-wide reusable logic and types
- Application contains business-logic for use cases and types
- Infrastructure contains all external concerns
- Presentation and Infrastructure depend only on Application
- Infrastructure and Presentation components can be replaced with minimal effort

# Application layer and  CQRS
Application layer is for use case scenarios to be implemented. The CQRS structure in [Figure 2] shows clear messages of the application purpose. 

[Figure 2] - CQRS Folder Structure

![CQRS structure](Documents/cqrs.png)

Having interfaces declared in Application conforms to the inversion of control. It makes it easy to replace external resource (dependency). [Figure 3] shows an example of creating a payment. 

[Figure 3]

![dependency flow](Documents/create-payment.png)

Handler acquires a bank client through a bank client factory to send a payment request to a bank. If it is successful, it saves the payment details using the application-db-context.
Note that Handler works with interfaces. 

Looking at arrows indicating the use of interfaces, nothing in Application knows anything about something in an outside world except Domain.

#Solution Architecture

[Figure 4]

![dependency flow](Documents/solution-architecture.png)

| Project        | Description           |
| ------------- |-------------|
| ApiClient     | A test tool to demonstrate how to get a bearer token form IdentityServer and use it to call API |
| Api     | Payment gateway API      |
| Infrastructure | Has MachineDateTime implementation of IDateTime from Common project     |
| Bank | Collection of bank clients. Bank simulator mocks a bank for the sake of test |
| DbMigration | Control database schema explicitly. Run by a build script |
| IdentityServer | Issue a bearer token to secure API |
| Persistence |  |
| Application |  |
| Domain |  |
| Common |  |


