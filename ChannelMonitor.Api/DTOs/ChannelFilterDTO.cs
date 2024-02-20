using ChannelMonitor.Api.Utilities;

namespace ChannelMonitor.Api.DTOs
{
    public class ChannelFilterDTO
    {
        public bool Alerted { get; set; }
        public string? Name { get; set; }

        public static ValueTask<ChannelFilterDTO> BindAsync(HttpContext context)
        {
            var alerted = context.ExtraerValorODefecto(nameof(Alerted), false);
            var name = context.ExtraerValorODefecto(nameof(Name), string.Empty);

            var resultado = new ChannelFilterDTO
            {
                Alerted = alerted,
                Name = name
            };

            return ValueTask.FromResult(resultado);

        }

    }
}
