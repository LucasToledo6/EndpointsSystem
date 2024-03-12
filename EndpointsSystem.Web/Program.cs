using EndpointSystem.Application.Input.Model;
using EndpointSystem.Application.Input.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add validators to the container.
builder.Services.AddScoped<IValidator<CreateEndpointInput>, CreateEndpointInputValidator>();

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
