using ProgMapApi;
using Npgsql;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load(); // loads from .env

var connStr = $"Host={Environment.GetEnvironmentVariable("PG_HOST")};" +
              $"Port={Environment.GetEnvironmentVariable("PG_PORT")};" +
              $"Database={Environment.GetEnvironmentVariable("PG_DB")};" +
              $"Username={Environment.GetEnvironmentVariable("PG_USER")};" +
              $"Password={Environment.GetEnvironmentVariable("PG_PASSWORD")}";

Console.WriteLine(connStr);
builder.Services.AddNpgsqlDataSource(
    connStr
);

var app = builder.Build();
// app.UseHttpsRedirection();

app.MapBandsEndpoints();
app.MapUpdateEndpoint();

app.Run();