using expenseTracker_Api.Data;
using expenseTracker_Api.Repositories;
using expenseTracker_Api.Service;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting connection string
builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("DefaultConnection"));

//Injecting Repositories and context
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<usersRepository>();
builder.Services.AddSingleton<categoriesRepository>();
builder.Services.AddSingleton<expensesRepository>();
builder.Services.AddSingleton<monthlySummariesRepository>();

//Injecting Services
builder.Services.AddHostedService<monthSummaryService>();

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
