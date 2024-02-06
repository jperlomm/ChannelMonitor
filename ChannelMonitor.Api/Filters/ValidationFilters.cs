
using ChannelMonitor.Api.DTOs;
using FluentValidation;

namespace ChannelMonitor.Api.Filters
{
    public class ValidationFilters<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator is null) return await next(context);

            var ForValidate = context.Arguments.OfType<T>().FirstOrDefault();
            if (ForValidate is null) return TypedResults.Problem("No pudo ser encontrada la entidad a validar");

            var resultValidator = await validator.ValidateAsync(ForValidate);
            if (!resultValidator.IsValid) return TypedResults.ValidationProblem(resultValidator.ToDictionary());

            return await next(context);
        }
    }
}
