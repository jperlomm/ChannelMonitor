using ChannelMonitor.Api;
using ChannelMonitor.Api.Endpoints;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Repositories;
using ChannelMonitor.Api.Services;
using ChannelMonitor.Api.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var allowOrigins = builder.Configuration.GetValue<string>("allowOrigins")!;

// Servicios

builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

// Para crear y manejar usuarios
builder.Services.AddScoped<UserManager<IdentityUser>>();
// Para logear usuarios
builder.Services.AddScoped<SignInManager<IdentityUser>>();

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(allowOrigins).AllowAnyHeader().AllowAnyMethod();
    });

});

builder.Services.AddOutputCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositorioChannel, RepositorioChannel>();
builder.Services.AddScoped<IRepositorioAlertStatus, RepositorioAlertStatus>();
builder.Services.AddScoped<IRepositorioChannelDetail, RepositorioChannelDetail>();
builder.Services.AddScoped<IRepositorioErrors, RepositorioErrors>();

builder.Services.AddTransient<IUsersServices, UsersServices>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));

// Fluent validation.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Para el manejo de errores cuando ocurre alguna excepcion.
builder.Services.AddProblemDetails();

// Para autorizacion y roles.
builder.Services.AddAuthentication().AddJwtBearer(options => 
{
    // Para que no haga el mapeo de los claims (les cambia el nombre de clave).
    options.MapInboundClaims = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = Keys.GetKey(builder.Configuration).First(), // Usar solo una llave
        IssuerSigningKeys = Keys.GetAllKeys(builder.Configuration), // Usar multiples llaves
        ClockSkew = TimeSpan.Zero
    };

});
builder.Services.AddAuthorization(options =>
{
    // Agregamos politica de administrador.
    options.AddPolicy("isadmin", policy => policy.RequireClaim("isadmin"));
});

// Servicios
var app = builder.Build();
// Middleware

app.UseSwagger();
app.UseSwaggerUI();

// Para el manejo de errores cuando ocurre alguna excepcion. Guardado en DB.
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
{
    var exceptionHandleFeature = context.Features.Get<IExceptionHandlerFeature>();
    var excepcion = exceptionHandleFeature?.Error!;

    var error = new Error();
    error.Date = DateTime.UtcNow;
    error.Message = excepcion.Message;
    error.StackTrace = excepcion.StackTrace;

    var repositorio = context.RequestServices.GetRequiredService<IRepositorioErrors>();
    await repositorio.Create(error);

    await TypedResults.BadRequest(
        new { tipo = "error", mensaje = "Ha ocurrido un mensaje de error inesperado", estatus = 500 })
    .ExecuteAsync(context);

}));
app.UseStatusCodePages(); // Para retornar codigos de status cuando haya error.

app.UseStaticFiles();

app.UseCors();
app.UseOutputCache();

app.UseAuthorization();

app.MapGroup("/channels").MapChannels();
app.MapGroup("/users").MapUsers();

// Middleware
app.Run();
