using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Filters;
using ChannelMonitor.Api.Services;
using ChannelMonitor.Api.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChannelMonitor.Api.Endpoints
{
    public static class UsersEndpoints
    {
        public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
        {
            group.MapPost("/register", Register)
                .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>();

            group.MapPost("/login", Login)
                .AddEndpointFilter<ValidationFilters<LoginUserCredentialsDTO>>();

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
            [FromServices] UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            var usuario = new ApplicationUser
            {
                UserName = userCredentialsDTO.UserName,
                TenantId = userCredentialsDTO.TenantId
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
            LoginUserCredentialsDTO userCredentialsDTO, [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromServices] UserManager<ApplicationUser> userManager, IConfiguration configuration)
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
                    await CreateTokenLogin(userCredentialsDTO, configuration, userManager, usuario);
                return TypedResults.Ok(respuestaAutenticacion);
            }
            else
            {
                return TypedResults.BadRequest("Login incorrecto");
            }
        }

        private async static Task<ResponseAuthenticationDTO> CreateToken(UserCredentialsDTO userCredentialsDTO,
            IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {

            var claims = new List<Claim>
            {
                new Claim("username", userCredentialsDTO.UserName),
                new Claim("tenant_id", userCredentialsDTO.TenantId.ToString())
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

        private async static Task<ResponseAuthenticationDTO> CreateTokenLogin(LoginUserCredentialsDTO userCredentialsDTO,
            IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationUser applicationUser)
        {

            var claims = new List<Claim>
            {
                new Claim("username", userCredentialsDTO.UserName),
                new Claim("tenant_id", applicationUser.TenantId.ToString())
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
            [FromServices] UserManager<ApplicationUser> userManager)
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
            [FromServices] UserManager<ApplicationUser> userManager)
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
            [FromServices] UserManager<ApplicationUser> userManager)
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
