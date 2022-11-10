using app.Context;
using app.Servicios.Departamentos;
using app.Servicios.Empleados;
using app.Servicios.Nominas;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IServicioDepartamento, ServicioDepartamento>();
builder.Services.AddScoped<IServicioEmpleado, ServicioEmpleado>();
builder.Services.AddScoped<IServicioNomina, ServicioNomina>();
builder.Services.AddScoped<IServicioNominaDetalle, ServicioNominaDetalle>();



builder.Services.AddDbContext<BDContext>(options => 
		options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));


builder.Services.AddCors(options =>
{
	options.AddPolicy( name: "PermitirOrigenes",
		builder =>
		{
			builder.AllowAnyOrigin()
				   .AllowAnyHeader()
				   .AllowAnyMethod();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("PermitirOrigenes");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
