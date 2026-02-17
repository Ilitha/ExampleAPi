using ExampleAPi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Razor Pages support for UI
builder.Services.AddRazorPages();

// Register Weather Service with HttpClient
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddHttpClient<ILocationService, LocationService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ExampleAPI v1");
        options.RoutePrefix = "swagger"; // Move Swagger to /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Redirect root to Index page
//app.MapGet("/", () => Results.Redirect("/Index"));
app.MapGet("/", (HttpContext ctx) =>
{
    var qs = ctx.Request.QueryString.HasValue ? ctx.Request.QueryString.Value : "";
    return Results.Redirect("/Index" + qs, permanent: false);
});


// Program.cs  (place BEFORE app.MapRazorPages();)
// This will prove whether the browser ever hits /Index?latitude=...&longitude=...
app.Use(async (ctx, next) =>
{
    var logger = ctx.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("REQUEST");
    logger.LogInformation("REQUEST {Method} {Path}{Query} | Referer={Referer}",
        ctx.Request.Method,
        ctx.Request.Path,
        ctx.Request.QueryString,
        ctx.Request.Headers.Referer.ToString());

    await next();
});

app.MapRazorPages();
app.MapControllers();

app.Run();

