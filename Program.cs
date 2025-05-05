var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

List<string> cityNames = [
    "London", "New York", "Tokyo", "Sydney", "Berlin", "Paris", "Moscow", "Beijing", "Cairo", "Rio de Janeiro"
];

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  new WeatherForecast() {
        Date = DateOnly.FromDateTime(DateTime.Now),
        Cities = cityNames.Take(5).Select(cityName => new City
        {
            Name = cityName,
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToList(),
        CitiesMore = cityNames.Skip(5).Select(cityName => new City
        {
            Name = cityName,
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToList()
    };
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record City
{
    public required string Name { get; init; }
    public required int TemperatureC { get; init; }
    public required string Summary { get; init; }
}

record WeatherForecast
{
    public DateOnly Date { get; init; }
    public required List<City> Cities { get; init; }
    public List<City>? CitiesMore { get; init; }
}
