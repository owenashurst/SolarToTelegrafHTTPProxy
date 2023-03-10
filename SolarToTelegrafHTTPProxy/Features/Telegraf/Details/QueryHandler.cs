using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf.Details
{
    public class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly ITelegrafHttpService _telegrafHttpService;

        public QueryHandler(ITelegrafHttpService telegrafHttpService)
        {
            _telegrafHttpService = telegrafHttpService;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _telegrafHttpService.SubmitToTelegraf(request);
            if (result)
            {
                return new Response { Success = true };
            } 
            else
            {
                return new Response { Success = false };
            }
        }
    }
}
