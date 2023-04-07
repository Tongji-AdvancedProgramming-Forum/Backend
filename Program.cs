using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Forum.Entity;
using Forum.Rest;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ?? "Not Found MySQL Connection Settings!!!";
builder.Services.AddDbContext<ForumDb>(options => options.UseMySQL(connectionString));

var app = builder.Build();

Rest_Register.Register(app);

app.Run();
