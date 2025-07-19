using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;
using DbUp.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();

var config = host.Services.GetRequiredService<IConfiguration>();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? config.GetConnectionString("DefaultConnection");

var upgrader = DeployChanges.To
    .PostgresqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("Nertz.Migrations.PreDeployment."), new SqlScriptOptions { RunGroupOrder = 1, ScriptType = ScriptType.RunAlways})
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), scripts => scripts.StartsWith("Nertz.Migrations.Scripts."), new SqlScriptOptions { RunGroupOrder = 2, ScriptType = ScriptType.RunAlways })
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), scripts => scripts.StartsWith("Nertz.Migrations.PostDeployment."), new SqlScriptOptions { RunGroupOrder = 3, ScriptType = ScriptType.RunAlways })
    .JournalTo(new NullJournal())
    .LogToConsole()
    .Build();

 EnsureDatabase.For.PostgresqlDatabase(connectionString);
var isUpgradeRequired = upgrader.IsUpgradeRequired();

if (!isUpgradeRequired)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Database is up to date!");
    Console.ResetColor();
    return 0;   
}

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return -1;
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Database migration successful!");
    Console.ResetColor();
    return 0;
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true,
                reloadOnChange: true);
            config.AddEnvironmentVariables();
            config.AddUserSecrets<Program>();
        });
}