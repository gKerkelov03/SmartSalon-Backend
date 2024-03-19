using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSalon.Data;
using SmartSalon.Presentation.Web.Controllers.V1;
using Xunit;
using Testcontainers.MsSql;
using Microsoft.EntityFrameworkCore;

namespace SmartSalon.Tests.Integration;

public class SmartSalonApiFactory : WebApplicationFactory<ApiController>, IAsyncLifetime
{
    private readonly MsSqlContainer dbContainer = new MsSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(SmartSalonDbContext));
            services.AddDbContext<SmartSalonDbContext>(options
                => options.UseSqlServer(dbContainer.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
        => await dbContainer.StartAsync();

    public new async Task DisposeAsync()
        => await dbContainer.StopAsync();
}
