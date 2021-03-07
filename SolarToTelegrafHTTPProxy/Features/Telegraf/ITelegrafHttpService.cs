using System.Threading.Tasks;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf
{
    public interface ITelegrafHttpService
    {
        Task<bool> SubmitToTelegraf(Details.Query query);
    }
}
