using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Repositories;
using FluentValidation;
using System.Runtime;

namespace ChannelMonitor.Api.Validation
{
    public class CreateFailureLoggingDTOValidation : AbstractValidator<CreateFailureLoggingDTO>
    {
        public CreateFailureLoggingDTOValidation(IRepositorioChannel repositorioChannel)
        {
            // Cuando tenemos que por ej consulta a db es MustAsync.
            RuleFor(x => x.ChannelId).NotEmpty().WithMessage(Utilities.Required)
            .MustAsync(async (idChannel, _) =>
            {
                // Retornar falso indica que la valicion no fue complida.
                return (await repositorioChannel.Exist(Convert.ToInt32(idChannel)));

            }).WithMessage(Utilities.NoExistMessage);

        }
    }
}
