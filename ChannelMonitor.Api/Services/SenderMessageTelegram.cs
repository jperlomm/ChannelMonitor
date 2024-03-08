
using System.Net;
using Telegram.Bot;

namespace ChannelMonitor.Api.Services
{
    public class SenderMessageTelegram : ISenderMessage
    {
        private readonly string _apiTokenTelegram;

        public SenderMessageTelegram(IConfiguration configuration)
        {
            _apiTokenTelegram = configuration.GetValue<string>("apiTokenTelegram")!;
        }

        public async Task SendMessage(string content, string destination)
        {
            // Seteamos variable _botClient.
            ITelegramBotClient _botClient;

            // Se especifica el protocolo de seguridad a utilizar.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Instanciamos TelegramBotClient.
            _botClient = new TelegramBotClient(_apiTokenTelegram);

            await _botClient.SendTextMessageAsync(destination, content);

        }
    }
}
