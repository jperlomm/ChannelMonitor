using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Filters;
using ChannelMonitor.Api.Services;
using ChannelMonitor.Api.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChannelMonitor.Api.Endpoints
{
    public static class UsersEndpoints
    {
        public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
        {
            group.MapPost("/register", Register)
                .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>();

            group.MapPost("/login", Login)
                .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>();

            group.MapPost("/setadmin", SetAdmin)
                .AddEndpointFilter<ValidationFilters<EditClaimDTO>>()
                .RequireAuthorization("isadmin");

            group.MapPost("/removeadmin", RemoveAdmin)
               .AddEndpointFilter<ValidationFilters<EditClaimDTO>>()
               .RequireAuthorization("isadmin");

            group.MapGet("/renewtoken", RenewToken).RequireAuthorization();

            return group;
        }

        static async Task<Results<Ok<ResponseAuthenticationDTO>, BadRequest<IEnumerable<IdentityError>>>>
            Register(UserCredentialsDTO userCredentialsDTO,
            [FromServices] UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var usuario = new IdentityUser
            {
                UserName = userCredentialsDTO.UserName
            };

            var resultado = await userManager.CreateAsync(usuario, userCredentialsDTO.Password);

            if (resultado.Succeeded)
            {
                var credencialesRespuesta =
                    await CreateToken(userCredentialsDTO, configuration, userManager);
                return TypedResults.Ok(credencialesRespuesta);
            }
            else
            {
                return TypedResults.BadRequest(resultado.Errors);
            }
        }

        static async Task<Results<Ok<ResponseAuthenticationDTO>, BadRequest<string>>> Login(
            UserCredentialsDTO userCredentialsDTO, [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var usuario = await userManager.FindByNameAsync(userCredentialsDTO.UserName);            

            if (usuario is null)
            {
                return TypedResults.BadRequest("Login incorrecto");
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(usuario,
                userCredentialsDTO.Password, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var respuestaAutenticacion =
                    await CreateToken(userCredentialsDTO, configuration, userManager);
                return TypedResults.Ok(respuestaAutenticacion);
            }
            else
            {
                return TypedResults.BadRequest("Login incorrecto");
            }
        }

        private async static Task<ResponseAuthenticationDTO> CreateToken(UserCredentialsDTO userCredentialsDTO,
            IConfiguration configuration, UserManager<IdentityUser> userManager)
        {

            var claims = new List<Claim>
            {
                new Claim("username", userCredentialsDTO.UserName)
            };

            var usuario = await userManager.FindByNameAsync(userCredentialsDTO.UserName);
            var claimsDB = await userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsDB);

            var llave = Keys.GetKey(configuration);
            var creds = new SigningCredentials(llave.First(), SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

            return new ResponseAuthenticationDTO
            {
                Token = token,
                Expiration = expiration
            };
        }

        static async Task<Results<NoContent, NotFound>> SetAdmin(EditClaimDTO editClaimDTO,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuario = await userManager.FindByNameAsync(editClaimDTO.UserName);
            if (usuario is null)
            {
                return TypedResults.NotFound();
            }

            await userManager.AddClaimAsync(usuario, new Claim("isadmin", "true"));
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> RemoveAdmin(EditClaimDTO editClaimDTO,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuario = await userManager.FindByNameAsync(editClaimDTO.UserName);
            if (usuario is null)
            {
                return TypedResults.NotFound();
            }

            await userManager.RemoveClaimAsync(usuario, new Claim("isadmin", "true"));
            return TypedResults.NoContent();
        }

        public async static Task<Results<Ok<ResponseAuthenticationDTO>, NotFound>> RenewToken(
            IUsersServices usersServices, IConfiguration configuration,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuario = await usersServices.GetUser();

            if (usuario is null)
            {
                return TypedResults.NotFound();
            }

            var userCredentialsDTO = new UserCredentialsDTO { UserName = usuario.UserName! };

            var respuestaAutenticacionDTO = await CreateToken(userCredentialsDTO, configuration,
                userManager);

            return TypedResults.Ok(respuestaAutenticacionDTO);

        }

    }
}
