using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Repositories;
using FluentValidation;

namespace ChannelMonitor.Api.Validation
{
    public class UpdateChannelValidation : AbstractValidator<UpdateChannelDTO>
    {
        public UpdateChannelValidation(IRepositorioAlertStatus repositorioAlertStatus, IRepositorioChannelDetail repositorioChannelDetail)
        {

            // Cuando tenemos que por ej consulta a db es MustAsync.
            RuleFor(x => x.AudioFailureId).MustAsync(async (audioFailureId, _) =>
            {
                if (audioFailureId != null)
                {
                    // Retornar falso indica que la valicion no fue complida.
                    return (await repositorioAlertStatus.Exist(Convert.ToInt32(audioFailureId)));
                }

                return true;

            }).WithMessage(Utilities.NoExistMessage);

            RuleFor(x => x.VideoFailureId).MustAsync(async (videoFailureId, _) =>
            {
                if (videoFailureId != null)
                {
                    // Retornar falso indica que la valicion no fue complida.
                    return (await repositorioAlertStatus.Exist(Convert.ToInt32(videoFailureId)));
                }

                return true;

            }).WithMessage(Utilities.NoExistMessage);

            RuleFor(x => x.GeneralFailureId).MustAsync(async (generalFailureId, _) =>
            {
                if (generalFailureId != null)
                {
                    // Retornar falso indica que la valicion no fue complida.
                    return (await repositorioAlertStatus.Exist(Convert.ToInt32(generalFailureId)));
                }

                return true;

            }).WithMessage(Utilities.NoExistMessage);

            RuleFor(x => x.ChannelDetailsId).MustAsync(async (channelDetailsId, _) =>
            {
                if (channelDetailsId != null)
                {
                    // Retornar falso indica que la valicion no fue complida.
                    return (await repositorioChannelDetail.Exist(Convert.ToInt32(channelDetailsId)));
                }

                return true;

            }).WithMessage(Utilities.NoExistMessage);

        }
    }
}
