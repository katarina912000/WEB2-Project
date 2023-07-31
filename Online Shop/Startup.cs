using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Online_Shop.DataBaseContext;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Mapping;
using Online_Shop.Repository;
using Online_Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online_Shop", Version = "v1" });
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            //dodajem za bazu
            services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OnlineShop")),ServiceLifetime.Scoped);

            services.AddScoped<IUser, UserService>();
            services.AddScoped<IUserRepo, UserRepository>();
           

            

            
            //dodajem za mapiranje sa vezbi
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});
            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            services.AddCors(options =>
            {
                options.AddPolicy("ReactAppPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:3001", "https://localhost:3001", "http://localhost:3002", "https://localhost:3002", "https://localhost:44312")

                            .SetIsOriginAllowed(origin => true)
                           .AllowAnyMethod()
                            .AllowAnyHeader()
                             .AllowCredentials();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online_Shop v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("ReactAppPolicy");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
