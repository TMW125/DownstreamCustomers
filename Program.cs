using Microsoft.OpenApi.Models;
using DownstreamCustomers.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Downstream Customers API", Description = "Counting", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Downstream Customers API");
    });
}
app.MapPost("/downstream", (Request request) => DownstreamCustomersDB.GetDownCust(request));
app.Run();