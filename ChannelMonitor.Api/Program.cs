var builder = WebApplication.CreateBuilder(args);

var allowOrigins = builder.Configuration.GetValue<string>("allowOrigins")!;

// Servicios

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(allowOrigins).AllowAnyHeader().AllowAnyMethod();
    });

});

builder.Services.AddOutputCache();
builder.Services.AddSwaggerGen();

// Servicios
var app = builder.Build();
// Middleware

app.UseCors();
app.UseOutputCache();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

// Middleware
app.Run();
