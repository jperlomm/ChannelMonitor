using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChannelMonitor.Api.Endpoints
{
    public static class ContactsTenantEndpoints
    {
        public static RouteGroupBuilder MapContactsTenant(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).RequireAuthorization().WithOpenApi();

            return group;

        }

        static async Task<Ok<List<ContactsTenantDTO>>> GetAll
            (IRepositorioContactsTenant repositorio, IMapper mapper)
        {
            var contactsTenant = await repositorio.GetAll();
            var contactsTenantDTO = mapper.Map<List<ContactsTenantDTO>>(contactsTenant);
            return TypedResults.Ok(contactsTenantDTO);
        }
    }
}
