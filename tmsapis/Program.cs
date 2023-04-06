using Microsoft.EntityFrameworkCore;
using Commons.Models.Generico;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Commons.Models;
using Commons.Models.Administracion;
using Commons.Helpers;
using Commons.Utilerias;
using System.Configuration;
using Keycloak.AuthServices.Authentication;
using CloudinaryDotNet;
using Commons.Models.Utilites;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<HeaderGlobales>();
});

builder.Services.AddDbContext<adminDBContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    });

builder.Services.AddDbContext<genericoDBContext>();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<SingletonClientes>((sd) =>
{
    return new SingletonClientes(new adminDBContext());
});

//builder.Services.AddScoped(typeof(IRepositoryGeneric<>),typeof(RepositoryGeneric<>));
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//Keycloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);
//cloudinary
//builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySetting"));

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// Configure the HTTP request pipeline.
//if (app.Environment.)
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseHttpsRedirection();

//Keycloak
app.UseAuthentication();

app.UseAuthorization();

//if (app.Environment.IsDevelopment())
//{
    app.MapControllers();
//}
//else
//{
//    app.MapControllers().RequireAuthorization(); //Keycloak
//}
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.Run();
