
using System.Net;
using Telegram.Bot;

namespace ChannelMonitor.Api.Services
{
    public class SenderMessageTelegram : ISenderMessage
    {
        public async Task SendMessage(string content, string destination)
        {
            string apiTokenTelegram = "5265243375:AAFZjUcDdWgAmWtKhvlkftfDUalnzf9PVTU";

            // Seteamos variable _botClient.
            ITelegramBotClient _botClient;

            // Se especifica el protocolo de seguridad a utilizar.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Instanciamos TelegramBotClient.
            _botClient = new TelegramBotClient(apiTokenTelegram);

            await _botClient.SendTextMessageAsync(destination, content);

        }
    }
}
