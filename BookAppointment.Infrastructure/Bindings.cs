using BookAppointment.Infrastructure.Repositories;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookAppointment.Infrastructure
{
    public static class Bindings
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services) 
        {
            services.AddDbContext<AppointmentDbContext>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            return services;
        }
    }
}
