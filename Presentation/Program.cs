using Infrastructure.Persistance.Postgres;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

var defaultCorsPolicy = "default_cors_policy";

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        //prevent automatic 400-404 response
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configure all mappings
builder.Services.AddMappings();

builder.Services.AddDbContext<PostgresDbContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDbContext")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationServices();

builder.Services.AddCors(config =>
{
    var policy = new CorsPolicy();
    policy.Headers.Add("*");
    policy.Methods.Add("*");
    policy.Origins.Add("*");
    //policy.SupportsCredentials = true;
    config.AddPolicy(defaultCorsPolicy, policy);
});

builder.Services.ConfigureJwtAuthentication();
builder.Services.ConfigureJwtAuthorization();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(defaultCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
