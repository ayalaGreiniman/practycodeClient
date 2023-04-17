using Microsoft.OpenApi.Models;
using TodoApi;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbcontext>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3000")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });

});

var app = builder.Build();

app.UseCors("OpenPolicy");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});




app.MapGet("/", () => "Hello World!");

app.MapGet("/items", (ToDoDbcontext context) => {
    return context.Items.ToList();
});

app.MapPost("/items", async (ToDoDbcontext context, string name) => {
    Item item = new Item(name,false);
    context.Items.Add(item);
    await context.SaveChangesAsync();
    return item;       
    // return Results.Created($"/items/{context.IdItems}",  context);
});

app.MapPut("/items/{id}", async (ToDoDbcontext context, int id) => {
    var existItem=await context.Items.FindAsync(id);
    if(existItem is null)
        return Results.NotFound();
    // existItem.Name=item.Name;
    existItem.IsComplete=!existItem.IsComplete;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/items/{id}", async (ToDoDbcontext context, int id) => {
    var del= await context.Items.FindAsync(id);
    if(del is null)
        return Results.NotFound();
    context.Items.Remove(del);
    await context.SaveChangesAsync();
    return Results.NoContent();
});



app.Run();





