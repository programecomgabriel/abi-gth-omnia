# abi-gth-omnia

Developer Evaluation

## Overview

`abi-gth-omnia` is a .NET 8 Web API sample project for developer evaluation. It uses:

- ASP.NET Core Web API
- Entity Framework Core with PostgreSQL
- MediatR for request/response handling
- FluentValidation for request validation
- Serilog for logging
- Swagger for API documentation

## Prerequisites

- .NET 8 SDK
- Docker (optional, for container builds)
- PostgreSQL (or a reachable PostgreSQL instance)

## Repository layout

- `src/` - main application source projects
  - `Ambev.DeveloperEvaluation.WebApi` - API project
  - `Ambev.DeveloperEvaluation.ORM` - EF Core context and migrations
  - `Ambev.DeveloperEvaluation.Domain`, `Common`, `IoC`, `Application` - domain and supporting libraries
- `tests/` - unit, integration and functional tests
- `Dockerfile` - container image definition
- `coverage-report.sh` - helper script to generate test coverage report

## Configuration

The application reads configuration from the usual ASP.NET Core configuration providers. Important settings:

- `ConnectionStrings:DefaultConnection` - PostgreSQL connection string

You can set the connection string via environment variable:

`ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=dev;Username=postgres;Password=secret"`

You can set the ASP.NET Core environment by using the `ASPNETCORE_ENVIRONMENT` environment variable (e.g. `Development`).

## Local development

1. Restore and build:

`dotnet restore`

`dotnet build --configuration Release`

2. Apply EF Core migrations (from repo root):

`dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi`

3. Run the API (from repo root):

`dotnet run --project src/Ambev.DeveloperEvaluation.WebApi --launch-profile "Development"`

The API will expose Swagger when running in the Development environment at `/swagger`.

## API information

Base URL (local): `https://localhost:8080` or `http://localhost:8080` depending on launch configuration. The container image exposes ports `8080` and `8081`.

Swagger UI (Development):

- `GET /swagger` interactive OpenAPI UI

Health checks:

- `GET /health` or the configured health endpoints (readiness and liveness) check the application health

Authentication

This project uses JWT authentication. To call protected endpoints you must obtain a token (via an authentication endpoint) and send it in the `Authorization` header as `Bearer <token>`.

Typical authentication flow (examples):

- `POST /api/auth` authenticate (see `AuthController`) send `Email` and `Password` to receive a JWT
- `POST /api/users` create user (public endpoint used to register a user)

Example `curl` to authenticate (adjust path to actual implementation):

`curl -X POST https://localhost:8080/api/auth -H "Content-Type: application/json" -d '{"email":"test@example.com","password":"secret"}'`

The response should include a token you can use in subsequent requests, for example:

`curl -H "Authorization: Bearer <token>" https://localhost:8080/api/products`

## Implemented endpoints (actual)

The following controller routes and actions are implemented in the codebase. Use the Swagger UI to inspect models in detail.

Auth

- `POST /api/auth` authenticate user; input: `AuthenticateUserCommand` (`Email`, `Password`); response: `AuthenticateUserResult` (includes `Token`)

Users

- `POST /api/users` create user; input: `CreateUserCommand`; response: `CreateUserResult` (returns `Id`)
- `GET /api/users/{id}` get user by id; response: `GetUserResult` (Id, Name, Email, Phone, Role, Status)
- `DELETE /api/users/{id}` delete user by id

Products

- `GET /api/products` query products; accepts query string params (filter/sort/paging); response: paginated `GetProductsResult`
- `POST /api/products` create product; input: `CreateProductCommand`; response: `CreateProductResult`

## Example payloads

Create user:

`{
  "username": "jdoe",
  "password": "Secret123!",
  "email": "jdoe@example.com",
  "phone": "+5511999999999",
  "status": "Active",
  "role": "User"
}`

Authenticate:

`{
  "email": "jdoe@example.com",
  "password": "Secret123!"
}`

Create product:

`{
  "title": "Special Lager",
  "description": "Refreshing craft lager",
  "category": "Beer",
  "price": 9.99,
  "image": "https://example.com/image.jpg",
  "quantity": 150
}
`

## Developer guide follow this pattern

The project uses the following patterns. New features should follow them to keep consistency.

- Use MediatR for request handling: create a `Command`/`Query` object, a `Handler`, and optional FluentValidation validator.
- Keep controllers thin: controllers should only map HTTP requests to a MediatR command and return standardized API responses.
- Use `BaseController` helper methods and return `ApiResponse`/`ApiResponseWithData` wrappers.
- Register services and handlers in the IoC composition root (already configured in `Program.cs`).

Controller pattern (example):
```
[ApiController]
[Route("api/[controller]")]
public class ExampleController : BaseController
{
    private readonly IMediator _mediator;

    public ExampleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Dictionary<string,string> query, CancellationToken cancellationToken)
    {
        var cmd = new GetExampleCommand(query);
        var result = await _mediator.Send(cmd, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExampleCommand command, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(command, cancellationToken);
        return Created(string.Empty, new ApiResponseWithData<CreateExampleResult> { Success = true, Data = created });
    }
}
```

Command / Handler / Validator pattern (example):

Command
```
public class CreateExampleCommand : IRequest<CreateExampleResult>
{
    public string Name { get; set; } = string.Empty;
}
```

Handler
```
public class CreateExampleHandler : IRequestHandler<CreateExampleCommand, CreateExampleResult>
{
    private readonly IExampleRepository _repo;
    public CreateExampleHandler(IExampleRepository repo) => _repo = repo;

    public async Task<CreateExampleResult> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Example { Name = request.Name };
        await _repo.CreateAsync(entity, cancellationToken);
        return new CreateExampleResult { Id = entity.Id };
    }
}
```

Validator
```
public class CreateExampleValidator : AbstractValidator<CreateExampleCommand>
{
    public CreateExampleValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
```

Validation and pipeline

- Add validators to the Web API assembly; they are registered automatically using `AddValidatorsFromAssembly` in `Program.cs`.
- The project registers a `ValidationBehavior<TRequest,TResponse>` MediatR pipeline that returns validation errors as structured `ApiResponse.Errors`.

Response wrappers

- Use `ApiResponse` for responses without data and `ApiResponseWithData<T>` for responses with models. Controllers should use these types for consistent responses and status codes.

Testing and conventions

- Add unit tests for handlers and validators in `tests/Ambev.DeveloperEvaluation.Unit`.
- Add integration/functional tests under the respective test projects following existing patterns.

## Running with Docker

Build the image:

`docker build -t ambev-dev-eval .`

Run the container (example with environment variable for connection string):

`docker run -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=dev;Username=postgres;Password=secret" -p 8080:8080 ambev-dev-eval`

The Docker image listens on ports `8080` and `8081` as declared in the `Dockerfile`.

## Tests and Coverage

Run all tests:

`dotnet test`

Generate coverage report (requires `bash` and the script `coverage-report.sh`):

`bash coverage-report.sh`

The generated HTML report will be placed under `TestResults/CoverageReport/index.html`.

## Health checks

The project registers basic health checks. When running, check the configured health endpoint (see the WebApi project startup/configuration) to verify readiness.