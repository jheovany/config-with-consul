using Winton.Extensions.Configuration.Consul;

var builder = WebApplication.CreateBuilder(args);

// Obtiene valores de User Secrets o configuracion
var configuration = builder.Configuration;
var consulToken = configuration["Consul:Token"];

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Consul como IConfigurationProvider
builder.Configuration.AddConsul(
    "miapp/dev/appsettings.json", // Key en Consul
    options =>
    {
        options.ConsulConfigurationOptions = cco =>
        {
            cco.Address = new Uri("http://localhost:8500");
            cco.Token = consulToken;
        };

        options.Optional = true;
        options.ReloadOnChange = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Ejemplo: acceder a la config cargada desde Consul
app.MapGet("/mysetting", (IConfiguration config) =>
{
    var connString = config.GetConnectionString("DefaultConnection");
    var apiKey = config["ApiKeys:WeatherService"]; 
    return Results.Ok(new { ConnectionString = connString, ApiKey = apiKey });
});

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}