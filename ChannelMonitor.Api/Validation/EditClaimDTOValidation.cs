using ChannelMonitor.Api.DTOs;
using FluentValidation;

namespace ChannelMonitor.Api.Validation
{
    public class EditClaimDTOValidation : AbstractValidator<EditClaimDTO>
    {
        public EditClaimDTOValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(Utilities.Required)
                .MaximumLength(256).WithMessage(Utilities.MaxLenth);
        }
    }
}
