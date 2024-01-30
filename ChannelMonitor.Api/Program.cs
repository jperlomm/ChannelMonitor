var builder = WebApplication.CreateBuilder(args);
// Servicios

// Servicios
var app = builder.Build();
// Middleware

app.MapGet("/", () => "Hello World!");

// Middleware
app.Run();
