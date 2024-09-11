using Microsoft.EntityFrameworkCore;
using microTrading.Models;
using microTrading.RepositoriesEF;
using microTrading.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MicroTradingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("devConnectionString"));
});

//ajouter tous les services injectables à cette liste
builder.Services.AddScoped<ActiveService>();
builder.Services.AddScoped<ActiveRepository>();
builder.Services.AddScoped<WebSocketClientService>();
builder.Services.AddScoped<MessageHandlersService>();
builder.Services.AddScoped<ValueRecordService>();

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
