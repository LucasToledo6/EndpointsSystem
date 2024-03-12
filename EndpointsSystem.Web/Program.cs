using EndpointsSystem.Data.Repository.Implementation;
using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Data;
using EndpointSystem.Application.Input.Model;
using EndpointSystem.Application.Input.Validation;
using EndpointSystem.Application.Mappings;
using FluentValidation;
using EndpointSystem.Application.Services.Implementation;
using EndpointSystem.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEndpointService, EndpointService>();

// Add validators to the container.
builder.Services.AddScoped<IValidator<CreateEndpointInput>, CreateEndpointInputValidator>();

// Add databases and repositories
builder.Services.AddDbContext<EndpointDbContext>();
builder.Services.AddScoped<IEndpointRepository, EndpointRepository>();

// Add automapper mapping profiles
builder.Services.AddAutoMapper(typeof(EndpointMappingProfile));

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
