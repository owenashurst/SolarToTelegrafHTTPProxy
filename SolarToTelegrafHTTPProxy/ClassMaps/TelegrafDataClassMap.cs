using AutoMapper;
using SolarToTelegrafHTTPProxy.Features.Telegraf.Details;
using SolarToTelegrafHTTPProxy.Features.Telegraf.Models;

namespace SolarToTelegrafHTTPProxy.ClassMaps;

public class TelegrafDataClassMap : Profile
{
    public TelegrafDataClassMap()
    {
        CreateMap<Query, TelegrafData>();
    }
}