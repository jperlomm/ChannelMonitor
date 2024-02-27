using ChannelMonitor.Api.DTOs;
using FluentValidation;

namespace ChannelMonitor.Api.Validation
{
    public class UserCredentialsDTOValidation : AbstractValidator<UserCredentialsDTO>
    {
        public UserCredentialsDTOValidation() {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(Utilities.Required)
                .MaximumLength(256).WithMessage(Utilities.MaxLenth);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Utilities.Required);
        }
    }
}
