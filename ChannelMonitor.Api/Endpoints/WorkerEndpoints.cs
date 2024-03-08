using ChannelMonitor.Api.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using ChannelMonitor.Api.Repositories;

namespace ChannelMonitor.Api.Endpoints
{
    public static class WorkerEndpoints
    {
        public static RouteGroupBuilder MapWorkers(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).RequireAuthorization().WithOpenApi();

            return group;

        }

        static async Task<Ok<WorkerDTO>> GetAll
            (IRepositorioWorker repositorio, IMapper mapper)
        {
            var worker = await repositorio.GetAll();
            var workerDTO = mapper.Map<WorkerDTO>(worker);
            return TypedResults.Ok(workerDTO);
        }

    }
}
