using IMDBLib.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IMDBLib.Services.APIServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Access configuration from WebApplication instance
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IMovieService, MovieService>(); // Assuming MovieService implements IMovieService
builder.Services.AddScoped<IPersonService, PersonService>(); // Assuming PersonService implements IPersonService
builder.Services.AddDbContext<IMDBDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("IMDBConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
