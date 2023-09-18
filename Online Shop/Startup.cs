using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
using System.Text;
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
            var googleConfig = Configuration.GetSection("Webclient1");
            services.AddSingleton(googleConfig);

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online_Shop", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                  {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                   }
               
            });
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
            services.AddScoped<IProduct, ProductService>();
            services.AddScoped<IProductRepo, ProductRepository>();
            services.AddScoped<IOrder, OrderService>();
            services.AddScoped<IOrderRepo, OrderRepository>();
            services.AddScoped<IItemRepo, ItemRepository>();

            // services.AddHttpClient();
            services.AddCors(options =>
            {
                options.AddPolicy("ReactAppPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "https://localhost:3000",  "http://localhost:3001", "https://localhost:3001", "http://localhost:3002", "https://localhost:3002", "https://localhost:44312")

                            .SetIsOriginAllowed(origin => true)
                           .AllowAnyMethod()
                            .AllowAnyHeader()
                             .AllowCredentials();
                    });
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                options.AddPolicy("AllowAnonymousPolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.Identity == null || !context.User.Identity.IsAuthenticated));

                options.AddPolicy("VerifikovanProdavac", policy =>
                policy.RequireClaim("StatusApproval", "APPROVED"));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["jwtConfig:Issuer"],
                    ValidAudience = Configuration["jwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtConfig:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
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
            app.UseCors("ReactAppPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
        }
    }
}
