using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Services;
using System.Text;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddScoped<AuthHandlerService>();

            var key = Encoding.ASCII.GetBytes(_Configuration.GetValue<string>("AppSettings:Secret"));
            services.AddDbContext<DimensionDataAPIContext>(options =>
            options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var Datacontext = context.HttpContext.RequestServices.GetRequiredService<AuthHandlerService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var UserExists = Datacontext.UserExists(userId);
                        if (UserExists == false)
                        {
                                        // return unauthorized if user no longer exists
                                        context.Fail("User doesn't exist");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Company X API",
                    Description = "A API for Company X using Authentication and MVC CORE",
                    Contact = new OpenApiContact
                    {
                        Name = "Pieter van Zyl",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/pieter_the"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under CC BY-NC 3.0 ZA",
                        Url = new Uri("https://creativecommons.org/licenses/by-nc/3.0/za/"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //c.ExampleFilters();


                var filePath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
                c.IncludeXmlComments(filePath); // standard Swashbuckle functionality, this needs to be before c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>()

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                                                                              // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();

                // add Security information to each operation for OAuth2
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                // or use the generic method, e.g. c.OperationFilter<SecurityRequirementsOperationFilter<MyCustomAttribute>>();

                // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddSingleton<IConfiguration>(_Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company X");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
