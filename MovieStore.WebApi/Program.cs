using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbContexts;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Middlewares;
using MovieStore.WebApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//--------------------------------------------------------------------------
//SQL Server kullanacaðýmý ve Db pathimi vererek burada db'ye baðlanýyorum
builder.Services.AddDbContext<MovieStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});
//db context'i DI prensibine uygun ayaða kaldýrýrýz.
builder.Services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());
builder.Services.AddSingleton<ICustomLogger, CustomConsoleLogger>();
builder.Services.AddScoped<IActor, SActor>();

//automapper config
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//_________________________________________________________________________

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//------------custom middleware--------------------
app.UseCustomException();
//-------------------------------------------------

app.MapControllers();

app.Run();
