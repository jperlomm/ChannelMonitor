using ChannelMonitor.Api.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using ChannelMonitor.Api.Repositories;
using ChannelMonitor.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChannelMonitor.Api.Endpoints
{
    public static class WorkerEndpoints
    {
        public static RouteGroupBuilder MapWorkers(this RouteGroupBuilder group)
        {
            group.MapPut("/status/{id:int}", UpdateStatus).DisableAntiforgery().RequireAuthorization("ishealther").WithOpenApi();
            group.MapGet("/status", GetAll).RequireAuthorization().WithOpenApi();

            return group;

        }

        static async Task<Ok<UpdateWorkerDTO>> GetAll
            (IRepositorioWorker repositorio, IMapper mapper)
        {
            var workers = await repositorio.GetAll();
            var workersDTO = mapper.Map<UpdateWorkerDTO>(workers);
            return TypedResults.Ok(workersDTO);
        }

        static async Task<Results<NoContent, NotFound, ValidationProblem>> UpdateStatus(int id, [FromForm] UpdateWorkerDTO updateWorkerDTO,
            IRepositorioWorker repositorio, IMapper mapper, IUpdateEntitySignalR updateEntitySignalR)
        {
            var workerDb = await repositorio.GetById(id);
            if (workerDb is null) return TypedResults.NotFound();

            var updatedWorker = mapper.Map(updateWorkerDTO, workerDb);

            await repositorio.Update(updatedWorker);

            await updateEntitySignalR.SendUpdateWorkerStatus(updatedWorker);

            return TypedResults.NoContent();

        }

    }
}
