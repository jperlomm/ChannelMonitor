using ChannelMonitor.Api.DTOs;
using FluentValidation;

namespace ChannelMonitor.Api.Validation
{
    public class EditClaimDTOValidation : AbstractValidator<EditClaimDTO>
    {
        public EditClaimDTOValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(Utilities.Required)
                .MaximumLength(256).WithMessage(Utilities.MaxLenth)
                .EmailAddress().WithMessage(Utilities.EmailMessage);
        }
    }
}
