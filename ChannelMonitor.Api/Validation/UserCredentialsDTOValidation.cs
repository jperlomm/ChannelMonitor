using ChannelMonitor.Api.DTOs;
using FluentValidation;

namespace ChannelMonitor.Api.Validation
{
    public class UserCredentialsDTOValidation : AbstractValidator<UserCredentialsDTO>
    {
        public UserCredentialsDTOValidation() {
            RuleFor(x => x.Email).NotEmpty().WithMessage(Utilities.Required)
                .MaximumLength(256).WithMessage(Utilities.MaxLenth)
                .EmailAddress().WithMessage(Utilities.EmailMessage);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Utilities.Required);
        }
    }
}
