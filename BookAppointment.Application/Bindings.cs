using Microsoft.Extensions.DependencyInjection;

namespace BookAppointment.Application
{
    public static class Bindings
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Bindings).Assembly));
            return services;
        }
    }
}
