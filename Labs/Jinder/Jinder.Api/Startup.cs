using Jinder.Core.Services;
using Jinder.Dal;
using Jinder.Dal.Repositories;
using Jinder.Dal.Repositories.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

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
            services.TryAddSingleton<IAccessService, FreeAccessService>();
            services.TryAddSingleton<IUserService, UserService>();
            services.TryAddSingleton<ISummaryService, SummaryService>();
            services.TryAddSingleton<IVacancyService, VacancyService>();
            services.TryAddSingleton<ISummarySuggestionService, SummarySuggestionService>();
            services.TryAddSingleton<IVacancySuggestionService, VacancySuggestionService>();
            services.TryAddSingleton<IMatchService, MatchService>();
            services.TryAddSingleton<ISkillService, SkillService>();
            services.TryAddSingleton<ISpecializationService, SpecializationService>();

            services.TryAddSingleton<JinderContext, JinderContext>();

            services.TryAddSingleton<IUserRepository, UserRepository>();
            services.TryAddSingleton<ISkillRepository, SkillRepository>();
            services.TryAddSingleton<ISpecializationRepository, SpecializationRepository>();
            services.TryAddSingleton<ISummaryRepository, SummaryRepository>();
            services.TryAddSingleton<IVacancyRepository, VacancyRepository>();
            services.TryAddSingleton<ISummarySuggestionRepository, SummarySuggestionRepository>();
            services.TryAddSingleton<IVacancySuggestionRepository, VacancySuggestionRepository>();
            services.TryAddSingleton<IMatchRepository, MatchRepository>();


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