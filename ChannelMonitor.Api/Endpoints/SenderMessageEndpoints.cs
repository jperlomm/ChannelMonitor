using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChannelMonitor.Api.Endpoints
{
    public static class SenderMessageEndpoints
    {
        public static RouteGroupBuilder MapSenderMessages(this RouteGroupBuilder group)
        {
            group.MapPost("/sendmessage", SendMessage).DisableAntiforgery().RequireAuthorization().WithOpenApi();
            return group;
        }

        static async Task<Ok> SendMessage([FromForm] MessageDTO messageDTO, ISenderMessage sender, IMapper mapper)
        {
            await sender.SendMessage(messageDTO.Content, messageDTO.Destination);
            return TypedResults.Ok();
        }
    }
}
