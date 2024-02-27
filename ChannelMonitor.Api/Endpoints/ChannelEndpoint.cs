using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Filters;
using ChannelMonitor.Api.Repositories;
using ChannelMonitor.Api.Services;
using ChannelMonitor.Api.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ChannelMonitor.Api.Endpoints
{
    public static class ChannelEndpoint
    {
        public static RouteGroupBuilder MapChannels(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll);

            group.MapGet("/alarmed", GetAlarmedChannels);

            group.MapGet("/{id:int}", GetById);

            group.MapGet("/filter", GetByFilter).AddParameterChannelFilterAOpenAPI();

            group.MapPost("/", Create).DisableAntiforgery().RequireAuthorization().WithOpenApi(); ;

            group.MapPut("/{id:int}", Update).DisableAntiforgery().AddEndpointFilter<ValidationFilters<UpdateChannelDTO>>()
                .RequireAuthorization().WithOpenApi();

            group.MapDelete("/{id:int}", Delete).RequireAuthorization("isadmin");

            return group;

        }

        static async Task<Created<ChannelDTO>> Create([FromForm] CreateChannelDTO createChannelDTO,
            IRepositorioChannel repositorio, IOutputCacheStore outputCacheStore, IMapper mapper)
        {

            var channel = mapper.Map<Channel>(createChannelDTO);

            var id = await repositorio.Create(channel);
            await outputCacheStore.EvictByTagAsync("channel-get", default);
            var channelDTO = mapper.Map<ChannelDTO>(channel);
            return TypedResults.Created($"/channels/{id}", channelDTO);
        }

        static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, [FromForm] UpdateChannelDTO updateChannelDTO,
            IRepositorioChannel repositorio, IOutputCacheStore outputCacheStore, IMapper mapper, IRepositorioAlertStatus repositorioAlertStatus,
            IRepositorioChannelDetail repositorioChannelDetail, IUpdateEntitySignalR updateEntitySignalR)
        {
            var channelDb = await repositorio.GetById(id);
            if(channelDb is null) return TypedResults.NotFound();

            var updatedChannel = mapper.Map(updateChannelDTO, channelDb);

            await repositorio.Update(updatedChannel);
            await outputCacheStore.EvictByTagAsync("channel-get", default);

            if(updateChannelDTO.InProcessing != null || updateChannelDTO.MonitoringStartTime != null || 
               updateChannelDTO.MonitoringEndTime != null || updateChannelDTO.Port != null || 
               updateChannelDTO.Ip != null)
            {
                await updateEntitySignalR.SendUpdateEntity(repositorio, mapper, updatedChannel);
            }
            
            return TypedResults.NoContent();

        }

        static async Task<Ok<List<ChannelDTO>>> GetAll
            (IRepositorioChannel repositorio, IMapper mapper)
        {
            var channels = await repositorio.GetAll();
            var channelsDTO = mapper.Map<List<ChannelDTO>>(channels);
            return TypedResults.Ok(channelsDTO);
        }

        static async Task<Ok<List<ChannelDTO>>> GetAlarmedChannels
            (IRepositorioChannel repositorio, IMapper mapper)
        {
            var channels = await repositorio.GetAlarmedChannels();
            var channelsDTO = mapper.Map<List<ChannelDTO>>(channels);
            return TypedResults.Ok(channelsDTO);
        }

        static async Task<Results<Ok<ChannelDTO>, NotFound>> GetById(int id,
            IRepositorioChannel repositorio, IMapper mapper)
        {
            var channel = await repositorio.GetById(id);

            if (channel is null) return TypedResults.NotFound();

            var channelDTO = mapper.Map<ChannelDTO>(channel);

            return TypedResults.Ok(channelDTO);

        }

        static async Task<Results<NoContent, NotFound>> Delete(int id, IRepositorioChannel repositorio,
            IOutputCacheStore outputCacheStore)
        {
            var channelDB = await repositorio.GetById(id);

            if (channelDB is null)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Delete(id);
            await outputCacheStore.EvictByTagAsync("channel-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Ok<List<ChannelDTO>>> GetByFilter
            (ChannelFilterDTO channelFilterDTO, IRepositorioChannel repositorio, IMapper mapper)
        {
            var channels = await repositorio.Filter(channelFilterDTO);
            var channelsDTO = mapper.Map<List<ChannelDTO>>(channels);
            return TypedResults.Ok(channelsDTO);
        }

    }
}
