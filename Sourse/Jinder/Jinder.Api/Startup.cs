using Jinder.Core.Clients;
using Jinder.Core.Services;
using Jinder.Dal;
using Jinder.Dal.Repositories;
using Jinder.Dal.Repositories.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Jinder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<JinderContext>(options =>
                options.UseSqlServer(Configuration["connectionString:JinderDB"]));

            services.AddScoped<IAuthorizeClient>(x =>
                RestService.For<IAuthorizeClient>(Configuration["connectionString:JinderAuthHost"]));

            services.AddScoped<IAccessService, FreeAccessService>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISummaryService, SummaryService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<ISummarySuggestionService, SummarySuggestionService>();
            services.AddScoped<IVacancySuggestionService, VacancySuggestionService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ISpecializationService, SpecializationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            services.AddScoped<ISummaryRepository, SummaryRepository>();
            services.AddScoped<IVacancyRepository, VacancyRepository>();
            services.AddScoped<ISummarySuggestionRepository, SummarySuggestionRepository>();
            services.AddScoped<IVacancySuggestionRepository, VacancySuggestionRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}