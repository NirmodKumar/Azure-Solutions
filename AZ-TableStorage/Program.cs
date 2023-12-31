using AutoMapper;
using AZ_TableStorage.Models;
using AZ_TableStorage.TableStorageService;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
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


app.MapGet("/GetEntityFromPersonTableStorage", async (ITableStorageServiceProvider tableStorageServiceProvider, IMapper mapper, [FromQuery] string key) =>
{
    var result = await tableStorageServiceProvider.GetEntities(key);

    return Results.Ok(mapper.Map<List<PersonModel>>(result));
})
.WithName("GetEntityFromPersonTableStorage")
.WithOpenApi();

app.MapPut("/UpdateEntityFromPersonTableStorage", async (ITableStorageServiceProvider tableStorageServiceProvider, IMapper mapper, [FromQuery] string partitionKey,
    [FromQuery] string rowKey, [FromBody] PersonModel personModel) =>
{
    var result = await tableStorageServiceProvider.UpdateEntity(mapper.Map<PersonEntity>(personModel), partitionKey, rowKey);
    return Results.Ok(mapper.Map<PersonModel>(result));
})
.WithName("UpdateEntityFromPersonTableStorage")
.WithOpenApi();

app.MapDelete("/DeleteEntityFromPersonTableStorage", async (ITableStorageServiceProvider tableStorageServiceProvider, [FromQuery] string partitionKey, [FromQuery] string rowKey) =>
{
    await tableStorageServiceProvider.DeleteEntity(partitionKey, rowKey);

    return Results.Ok("Item Is Deleted!");
})
.WithName("DeleteEntityFromPersonTableStorage")
.WithOpenApi();

app.MapPost("/PostEntityToPersonTableStorage", async (ITableStorageServiceProvider tableStorageServiceProvider, IMapper mapper, [FromBody] PersonModel personModel) =>
{
    var result = await tableStorageServiceProvider.AddEntity(mapper.Map<PersonEntity>(personModel));

    return Results.Ok(mapper.Map<PersonModel>(result));
})
.WithName("PostEntityToPersonTableStorage")
.WithOpenApi();

app.Run();