using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf.Details
{
    public class QueryHandler : IRequestHandler<Query, Model>
    {
        private readonly ITelegrafHttpService _telegrafHttpService;

        public QueryHandler(ITelegrafHttpService telegrafHttpService)
        {
            _telegrafHttpService = telegrafHttpService;
        }

        public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _telegrafHttpService.SubmitToTelegraf(request);
            if (result)
            {
                return new Model { Success = true };
            } 
            else
            {
                return new Model { Success = false };
            }
        }
    }
}
