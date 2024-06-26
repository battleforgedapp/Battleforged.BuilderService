using System.Security.Claims;
using Battleforged.BuilderService.Domain.Repositories;
using Battleforged.BuilderService.Graph.Nodes;
using Battleforged.BuilderService.Graph.Queries;
using Battleforged.BuilderService.Helpers;
using Battleforged.BuilderService.Infrastructure.Database;
using Battleforged.BuilderService.Infrastructure.Database.Repositories;
using Battleforged.BuilderService.Json;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    // add our db context connection
    builder.Services.AddPooledDbContextFactory<AppDbContext>(cfg => {
        cfg.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        cfg.UseNpgsql(builder.Configuration.GetConnectionString("Default"), opts => {
            opts.MigrationsAssembly("Battleforged.BuilderService");
        });
    });
    
    // add our MediatR cqrs pipeline
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly
        //typeof(ImportSpreadsheetCommand).Assembly
    ));
    
    // configure the cors policy for development
    builder.Services.AddCors(cfg => {
        cfg.AddDefaultPolicy(plc => plc
            .WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts")!.Split("|"))
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
    });
    
    // configure our authentication
    builder.Services
        .AddAuthentication(o => {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(cfg => {
            // Authority is the URL of your clerk instance
            cfg.Authority = builder.Configuration["Clerk:Authority"];
            cfg.TokenValidationParameters = new TokenValidationParameters {
                // Disable audience validation as we aren't using it
                ValidateAudience = false,
                //NameClaimType = ClaimTypes.NameIdentifier 
            };
            cfg.Events = new JwtBearerEvents() {
                OnTokenValidated = context => {
                    var azp = context.Principal?.FindFirstValue("azp");
                    // AuthorizedParty is the base URL of your frontend.
                    if (string.IsNullOrEmpty(azp) || !azp.Equals(builder.Configuration["Clerk:AuthorizedParty"])) {
                        context.Fail("AZP Claim is invalid or missing");
                    }
                    return Task.CompletedTask;
                }
            };
        });
    
    // setup our repositories
    builder.Services.AddScoped<IEventOutboxRepository, EventOutboxRepository>();
    builder.Services.AddScoped<IRosterRepository, RosterRepository>();
    builder.Services.AddScoped<IRosterUnitRepository, RosterUnitRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    // handle our guid convertors a little better
    // configure newtonsoft to work better with fast-endpoints by configuring the settings better!
    JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
        Formatting = Formatting.Indented,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Converters = new List<JsonConverter>() {
            new GuidJsonConverter(),
            new NullableGuidJsonConverter()
        }
    };
    
    // configure the graphql server
    builder.Services
        .AddGraphQLServer()
        .RegisterDbContext<AppDbContext>()
        .AddAuthorization()
        .AddSorting()
        .AddQueryType(q => q.Name("Query"))
        .AddType<RosterQueries>()
        .AddTypeExtension<RosterNodes>();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors();
    app.UseEndpoints(e => {
        e.MapGraphQL();
        // e.MapFastEndpoints(cfg => {
        //     cfg.Versioning.Prefix = "v";
        //     cfg.Serializer.ResponseSerializer = (rsp, dto, cType, jCtx, ct) => {
        //         rsp.ContentType = cType;
        //         return rsp.WriteAsync(JsonConvert.SerializeObject(dto), ct);
        //     };
        //     cfg.Serializer.RequestDeserializer = async (req, tDto, jCtx, ct) => {
        //         using var reader = new StreamReader(req.Body);
        //         return JsonConvert.DeserializeObject(await reader.ReadToEndAsync(), tDto);
        //     };
        // });
    });
}

app.PreStartup().Run();