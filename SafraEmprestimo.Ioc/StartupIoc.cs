using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SafraEmprestimo.Data.Contextos;
using SafraEmprestimo.Data.Repositorios;
using SafraEmprestimo.Domain.Interfaces;
using SafraEmprestimo.Service.Emprestimos;

namespace SafraEmprestimo.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionString"]));
            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped(typeof(IEmprestimoRepositorio), typeof(EmprestimoRepositorio));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ArmazenadorDeEmprestimo>();
        }
    }
}
