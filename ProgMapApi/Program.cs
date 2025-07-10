using ProgMapApi;
using Npgsql;
using dotenv.net;
using ProgMapApi.DbHandler;
using ProgMapApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load(); // loads from .env

var connStr = $"Host={Environment.GetEnvironmentVariable("PG_HOST")};" +
              $"Port={Environment.GetEnvironmentVariable("PG_PORT")};" +
              $"Database={Environment.GetEnvironmentVariable("PG_DB")};" +
              $"Username={Environment.GetEnvironmentVariable("PG_USER")};" +
              $"Password={Environment.GetEnvironmentVariable("PG_PASSWORD")}";

IDbHandler dbHandler = new PgDbHandler(connStr);

var app = builder.Build();
// app.UseHttpsRedirection();

app.MapBandsEndpoints(dbHandler);
app.MapUpdateEndpoint();

app.Run();