using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace ChannelMonitor.Api.Endpoints
{
    public static class ChannelEndpoint
    {
        public static RouteGroupBuilder MapChannels(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).DisableAntiforgery();
            group.MapPost("/", Create).DisableAntiforgery();
            group.MapPut("/{id:int}", Update).DisableAntiforgery();

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

        static async Task<Results<NoContent, NotFound>> Update(int id, [FromForm] UpdateChannelDTO updateChannelDTO,
            IRepositorioChannel repositorio, IOutputCacheStore outputCacheStore, IMapper mapper, IRepositorioAlertStatus repositorioAlertStatus,
            IRepositorioChannelDetail repositorioChannelDetail)
        {
            var channelDb = await repositorio.GetById(id);
            if(channelDb is null) return TypedResults.NotFound();

            var updatedChannel = mapper.Map(updateChannelDTO, channelDb);

            // verificamos si el 'VideoFailureId', 'AudioFailureId' y 'GeneralFailureId' existen en 'AlertStatus'
            if (updateChannelDTO.VideoFailureId.HasValue && !await repositorioAlertStatus.Exist(updateChannelDTO.VideoFailureId.Value))
            {
                return TypedResults.NotFound();
            }

            if (updateChannelDTO.AudioFailureId.HasValue && !await repositorioAlertStatus.Exist(updateChannelDTO.AudioFailureId.Value))
            {
                return TypedResults.NotFound();
            }

            if (updateChannelDTO.GeneralFailureId.HasValue && !await repositorioAlertStatus.Exist(updateChannelDTO.GeneralFailureId.Value))
            {
                return TypedResults.NotFound();
            }

            // verificamos si 'ChannelDetails' existe
            if (updateChannelDTO.ChannelDetails != null && !await repositorioChannelDetail.Exist(updateChannelDTO.ChannelDetails.Id))
            {
                return TypedResults.NotFound();
            }

            await repositorio.Update(updatedChannel);
            await outputCacheStore.EvictByTagAsync("channel-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Ok<List<ChannelDTO>>> GetAll(IRepositorioChannel repositorio, IMapper mapper)
        {
            var channels = await repositorio.GetAll();
            var channelsDTO = mapper.Map<List<ChannelDTO>>(channels);
            return TypedResults.Ok(channelsDTO);
        }

    }
}
