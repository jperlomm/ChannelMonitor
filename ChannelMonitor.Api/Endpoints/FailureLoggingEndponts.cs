﻿using ChannelMonitor.Api.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using ChannelMonitor.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Filters;
using ChannelMonitor.Api.Services;

namespace ChannelMonitor.Api.Endpoints
{
    public static class FailureLoggingEndponts
    {
        private static readonly string contenedor = "image";

        public static RouteGroupBuilder MapFailureLogging(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).DisableAntiforgery();

            group.MapPost("/", Create)
                .DisableAntiforgery()
                .AddEndpointFilter<ValidationFilters<CreateFailureLoggingDTO>>()
                .WithOpenApi();

            return group;

        }

        static async Task<Results<Ok<List<FailureLoggingDTO>>, NotFound>> GetAll(int channelId,
            IRepositorioFailureLogging repositorioFailureLoggin, IRepositorioChannel repositorioChannel,
            IMapper mapper)
        {
            if (!await repositorioChannel.Exist(channelId)) return TypedResults.NotFound();

            var failureLogin = await repositorioFailureLoggin.GetAll(channelId);
            var failureLoginDTO = mapper.Map<List<FailureLoggingDTO>>(failureLogin);

            return TypedResults.Ok(failureLoginDTO);

        }

        static async Task<Created<FailureLoggingDTO>> Create([FromForm] CreateFailureLoggingDTO failureLoggingDTO,
            IRepositorioFailureLogging repositorio, IMapper mapper, IFileStorage iFileStorage)
        {

            var failureLoggin = mapper.Map<FailureLogging>(failureLoggingDTO);
            failureLoggin.DateFailure = DateTime.Now;

            if (failureLoggingDTO.File is not null)
            {
                var url = iFileStorage.Save(contenedor, failureLoggingDTO.File);
                failureLoggin.Url = await url;
            }

            var id = await repositorio.Create(failureLoggin);
            var failureLogginDTO = mapper.Map<FailureLoggingDTO>(failureLoggin);
            failureLoggin.Id = id;

            return TypedResults.Created($"/failureloggin/{id}", failureLogginDTO);

        }

    }
}
