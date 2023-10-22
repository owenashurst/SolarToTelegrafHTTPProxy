using System.Threading.Tasks;
using SolarToTelegrafHTTPProxy.Features.Telegraf.Models;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf
{
    public interface ITelegrafHttpService
    {
        Task<bool> SubmitToTelegraf(TelegrafData telegrafData);
    }
}
