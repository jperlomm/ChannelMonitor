
namespace ChannelMonitor.Api.Services
{
    public interface IFileStorage
    {
        Task Delete(string? path, string container);
        Task<string> Save(string conteiner, IFormFile file);
    }
}