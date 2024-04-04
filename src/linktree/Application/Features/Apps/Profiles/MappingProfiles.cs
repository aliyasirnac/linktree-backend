using Application.Features.Apps.Commands.Create;
using Application.Features.Apps.Commands.Delete;
using Application.Features.Apps.Commands.Update;
using Application.Features.Apps.Queries.GetById;
using Application.Features.Apps.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Apps.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<App, CreateAppCommand>().ReverseMap();
        CreateMap<App, CreatedAppResponse>().ReverseMap();
        CreateMap<App, UpdateAppCommand>().ReverseMap();
        CreateMap<App, UpdatedAppResponse>().ReverseMap();
        CreateMap<App, DeleteAppCommand>().ReverseMap();
        CreateMap<App, DeletedAppResponse>().ReverseMap();
        CreateMap<App, GetByIdAppResponse>().ReverseMap();
        CreateMap<App, GetListAppListItemDto>().ReverseMap();
        CreateMap<IPaginate<App>, GetListResponse<GetListAppListItemDto>>().ReverseMap();
    }
}