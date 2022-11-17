using ElasticSearchSample.API.Abstractions;
using ElasticSearchSample.API.Data;
using ElasticSearchSample.API.Extensions;
using ElasticSearchSample.API.Models;
using ElasticSearchSample.API.Services;

using Microsoft.EntityFrameworkCore;

using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    opt => opt.UseMySQL(builder.Configuration.GetConnectionString("MySql"))
);

builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddScoped(typeof(IDataRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IElasticSearchService<User>), typeof(UserSearchService));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();