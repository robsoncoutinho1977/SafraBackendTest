using SafraEmprestimo.Ioc;
using SafraEmprestimo.Web.Filters;
using SafraEmprestimo.Domain.Interfaces;

namespace SafraEmprestimo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            StartupIoc.ConfigureServices(services, Configuration);

            services.AddMvc(config => {
                config.Filters.Add(typeof(CustomExceptionFilter));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();

                var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
                await unitOfWork.Commit();
            });

            //app.UseBrowserLink();
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
        }
    }
}
