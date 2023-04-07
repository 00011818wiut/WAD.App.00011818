using System;
using Microsoft.EntityFrameworkCore;

using API.DAL;
using API.Domain;
using API.Repositories;
using API.Services;
using API.Security;
using Microsoft.Extensions.Options;

namespace API
{
    
    public class Startup
    {
        private readonly string _policyName = "PolicyName";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
      
            services.AddSwaggerGen();
            services.AddDbContext<DatabaseContext>(o => o.UseSqlite(
                Configuration
                .GetConnectionString("SQLITE_DB"))
                .ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning))
            );

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAuthService, AuthService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var routeProtector = new RouteProtector();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_policyName);
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseWhen(context => routeProtector.Check(context.Request.Method, context.Request.Path), appBuilder =>
            {
                appBuilder.UseMiddleware<AuthMiddleware>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}