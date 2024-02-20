using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace ChannelMonitor.Api.Utilities
{
    public static class SwaggerExtensions
    {
        public static TBuilder AddParameterChannelFilterAOpenAPI<TBuilder>(this TBuilder builder)
            where TBuilder : IEndpointConventionBuilder
        {
            return builder.WithOpenApi(opciones =>
            {

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "name",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                    }
                });

                opciones.Parameters.Add(new OpenApiParameter
                {
                    Name = "alerted",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean",
                    }
                });

                return opciones;

            });
        }
    }
}
