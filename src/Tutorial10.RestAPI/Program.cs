using Microsoft.EntityFrameworkCore;
using Tutorial10.RestAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");

builder.Services.AddDbContext<MasterContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/jobs", async (MasterContext context, CancellationToken cancellationToken ) =>
{
    try
    {
        return Results.Ok(await context.Jobs.ToListAsync(cancellationToken));
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }

});

app.MapGet("/api/departments", async (MasterContext context, CancellationToken cancellationToken) => {

    try
    {
        return Results.Ok(await context.Departemnts.ToListAsync(cancellationToken));
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
    
});

app.MapGet("/api/employees", async (MasterContext context, CancellationToken cancellationToken) =>
{
    try
    {
        var shortInfo = context.Employees.Select(e => new { e.Id, e.Name });
        return Results.Ok(await shortInfo.ToListAsync(cancellationToken));

    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }

});

app.MapGet("/api/employees/{id}", async (int id, MasterContext context, CancellationToken cancellationToken) =>
{
    
    
    
});

app.MapPost("/api/employees", () =>
{
    
});

app.MapPut("/api/employees/{id}", (int id) =>
{
    
});

app.MapDelete("/api/employees/{id}", (int id) =>
{
    
});

app.Run();
