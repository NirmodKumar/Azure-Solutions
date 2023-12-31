using AZ_TableStorage.Models;
using AZ_TableStorage.TableStorageService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITableStorageServiceProvider, TableStorageServiceProvider>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();


app.MapGet("/GetEntityFromPersonTableStorage/{key}", async (ITableStorageServiceProvider tableStorageServiceProvider, string key) =>
{
    var result = await tableStorageServiceProvider.GetEntities(key);

    return Results.Ok(result);
})
.WithName("GetEntityFromPersonTableStorage")
.WithOpenApi();

app.MapGet("/UpdateEntityFromPersonTableStorage", () =>
{

    return "";
})
.WithName("UpdateEntityFromPersonTableStorage")
.WithOpenApi();

app.MapDelete("/DeleteEntityFromPersonTableStorage/{partitionKey}/{rowKey}", async (ITableStorageServiceProvider tableStorageServiceProvider, string partitionKey, string rowKey) =>
{
    await tableStorageServiceProvider.DeleteEntity(partitionKey, rowKey);

    return Results.Ok("Item Is Deleted!");
})
.WithName("DeleteEntityFromPersonTableStorage")
.WithOpenApi();

app.MapPost("/PostEntityToPersonTableStorage", async (ITableStorageServiceProvider tableStorageServiceProvider, PersonEntity personEntity) =>
{
    var response = await tableStorageServiceProvider.AddEntity(personEntity);

    return Results.Ok(response);
})
.WithName("PostEntityToPersonTableStorage")
.WithOpenApi();

app.Run();