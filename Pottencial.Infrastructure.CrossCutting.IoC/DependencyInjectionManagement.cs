using Microsoft.Extensions.DependencyInjection;
using Pottencial.Domain.Interface;
using Pottencial.Domain.Services;

namespace Pottencial.Infrastructure.CrossCutting.DependecyInjection
{
    public static class DependencyInjectionManagement
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBlackListService, BlackListService>();
            services.AddScoped<IQuoteService, QuoteService>();
        }

        public static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            return services.BuildServiceProvider();
        }
    }
}
