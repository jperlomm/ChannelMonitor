using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
           
            CreateMap<CreateChannelDTO, Channel>();

            // Si algunas propiedades DTO son null se usan los valores predeterminados de la entidad
            CreateMap<CreateChannelDTO, Channel>()
                .ForAllMembers(opt => opt.UseDestinationValue());

            CreateMap<Channel, ChannelDTO>();

            CreateMap<Worker, UpdateWorkerDTO>();
            CreateMap<UpdateWorkerDTO, Worker>();

            CreateMap<ContactsTenant, ContactsTenantDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<string?, string>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<TimeSpan?, TimeSpan>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<UpdateChannelDTO, Channel>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<FailureLogging, FailureLoggingDTO>();
            CreateMap<CreateFailureLoggingDTO, FailureLogging>()
                .ForMember(x => x.Url, opciones => opciones.Ignore());

        }

    }
}
