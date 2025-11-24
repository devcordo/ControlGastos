using ControlGastosApp.Domain.Interfaces;
using ControlGastosApp.Infrastructure.Persistence;
using ControlGastosApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControlGastosApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<ITipoGastoRepository, TipoGastoRepository>();
            services.AddScoped<IFondoRepository, FondoRepository>();
            services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();
            services.AddScoped<IGastoRepository, GastoRepository>();
            services.AddScoped<IDepositoRepository, DepositoRepository>();

            return services;
        }
    }
}
