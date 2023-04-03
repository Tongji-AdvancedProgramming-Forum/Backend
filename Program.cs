using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("datasource") ?? "Data Source=Forum.db";
builder.Services.AddSqlite<ForumDb>(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
