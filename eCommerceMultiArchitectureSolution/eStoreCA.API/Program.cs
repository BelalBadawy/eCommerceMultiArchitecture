
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using eStoreCA.API.Filters;
using eStoreCA.API.Infrastructure;
using eStoreCA.API.Middlewares;
using eStoreCA.Application;
using eStoreCA.Infrastructure;
using eStoreCA.Infrastructure.Common;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Interfaces;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;



namespace eStoreCA.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Setup serilog in a two-step process. First, we configure basic logging
            // to be able to log errors during ASP.NET Core startup. Later, we read
            // log settings from appsettings.json. Read more at
            // https://github.com/serilog/serilog-aspnetcore#two-stage-initialization.
            // General information about serilog can be found at
            // https://serilog.net/


            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            //    .WriteTo.Console()   // .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            //    .CreateBootstrapLogger();


            try
            {
                //   Log.Information("Starting web host");


                builder.Services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(LogUserActivitiesAttribute));
                }).ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = InvalidModelStateResponse.MakeValidationResponse;
                }).AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);




                builder.Services.AddApplication();
                builder.Services.AddInfrastructure(builder.Configuration);

                builder.Services.AddHttpContextAccessor();

                builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                builder.Services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                    .AddApiExplorer(options =>
                    {
                        options.GroupNameFormat = "'v'VVV";
                        options.SubstituteApiVersionInUrl = true;
                    });

                builder.Services.AddAntiforgery();
                builder.Services.AddMemoryCache();
                builder.Services.AddDistributedMemoryCache();


                builder.Services.AddCors(options =>
                {
                    //.AllowAnyOrigin()
                    options.AddPolicy("Open",
                        builder => builder.WithOrigins(
                            "http://example.com",
                            "http://www.contoso.com")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            //  .AllowCredentials()
                            .AllowAnyOrigin());
                });

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {

                    options.MapType<byte[]>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                    {
                        Type = "string",
                        Format = "base64"
                    });

                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyTemplate", Version = "v1" });

                    var securitySchema = new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    };

                    options.AddSecurityDefinition("Bearer", securitySchema);

                    var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                    options.AddSecurityRequirement(securityRequirement);


                });



                builder.Services.Configure<IdentityOptions>(opt =>
                {
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequiredUniqueChars = 1;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                    opt.User.RequireUniqueEmail = true;
                    opt.SignIn.RequireConfirmedAccount = false;
                    opt.SignIn.RequireConfirmedEmail = false;
                    opt.SignIn.RequireConfirmedPhoneNumber = false;
                    opt.ClaimsIdentity.UserIdClaimType = "id";

                });

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero,
                       ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                       ValidAudience = builder.Configuration["JwtSettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                   };

               });


                builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtSettings"));
                builder.Services.Configure<CacheConfiguration>(builder.Configuration.GetSection("CacheConfiguration"));

                builder.Services.AddTransient<ICacheService, MemoryCacheService>();

                var app = builder.Build();

                //  app.UseSwaggerAuthorized();
                app.UseDeveloperExceptionPage();
                app.UseSwaggerAuthorized();
                app.UseSwagger();
                app.UseSwaggerUI();
                //}

                app.UseCors("Open");

                app.UseStatusCodePages(async context =>
                {
                    var request = context.HttpContext.Request;
                    var response = context.HttpContext.Response;

                    DefaultContractResolver contractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };

                    context.HttpContext.Response.ContentType = "application/json";

                    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        var responseUnauthorized = new MyAppResponse<int>("Unauthorized request " + response.StatusCode);
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseUnauthorized, new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        }));
                    }
                    else if (response.StatusCode == (int)HttpStatusCode.NotFound)
                    {
                        var responseNotFound = new MyAppResponse<int>("NotFound request path " + response.StatusCode);
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseNotFound, new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        }));
                    }
                    else if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
                    {
                        var responseNotFound = new MyAppResponse<int>("InternalServerError" + response.StatusCode);
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseNotFound, new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        }));
                    }
                    else if (response.StatusCode == (int)HttpStatusCode.BadRequest)
                    {
                        var responseNotFound = new MyAppResponse<int>("Bad Request" + response.StatusCode);
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseNotFound, new JsonSerializerSettings
                        {
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        }));
                    }

                });

                app.UseMiddleware<eStoreCA.API.Middlewares.ExceptionHandlerMiddleware>();

                app.UseHttpsRedirection();

                app.UseStaticFiles();

                app.UseAuthentication();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}



