using Ambev.DeveloperEvaluation.Domain.Users;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Testcontainers.PostgreSql;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.WebApi;

public class IntegrationTestWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:13").Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DefaultContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DefaultContext>(options => options.UseNpgsql(_postgres.GetConnectionString()));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TestUserBehavior<,>));
        });
    }

    public Task InitializeAsync()
    {
        return _postgres.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _postgres.StopAsync();
    }
}

public class TestUserBehavior<TRequest, TResponse>(IHttpContextAccessor accessor, IUserRepository userRepository) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private const string UserId = "e99a6267-2c7a-483f-b84a-aca9d0e8e74f";

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        accessor.HttpContext ??= new DefaultHttpContext();

        var user = await userRepository.GetByIdAsync(Guid.Parse(UserId), cancellationToken);

        if (user == null)
        {
            user = new User()
            {
                Id = Guid.Parse(UserId),
                Username = "testuser",
                Email = "testuser@testuser.com",
                Role = UserRole.Admin
            };

            user = await userRepository.CreateAsync(user, cancellationToken);
        }

        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        accessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "IntegrationTestWebApi"));

        return await next();
    }
}
