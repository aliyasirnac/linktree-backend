using Application.Features.Apps.Queries.GetById;
using Application.Features.Companies.Commands.Create;
using Application.Features.Companies.Commands.Delete;
using Application.Features.Companies.Commands.Update;
using Application.Features.Companies.Commands.UpdateCompanyImage;
using Application.Features.Companies.Queries.GetById;
using Application.Features.Companies.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Companies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Company, CreateCompanyCommand>().ReverseMap();
        CreateMap<Company, CreatedCompanyResponse>().ReverseMap();
        CreateMap<Company, UpdateCompanyCommand>().ReverseMap();
        CreateMap<Company, UpdatedCompanyResponse>().ReverseMap();
        CreateMap<Company, DeleteCompanyCommand>().ReverseMap();
        CreateMap<Company, DeletedCompanyResponse>().ReverseMap();
        CreateMap<Company, GetByIdCompanyResponse>()
            .ForMember(dest => dest.Apps, opt => opt.MapFrom(src => src.Apps))
            .ReverseMap();
        CreateMap<Company, GetListCompanyListItemDto>().ReverseMap();
        CreateMap<IPaginate<Company>, GetListResponse<GetListCompanyListItemDto>>().ReverseMap();
        CreateMap<Company, UpdateCompanyImageCommand>().ReverseMap();
        CreateMap<Company, UpdateCompanyImageResponse>().ReverseMap();
    }
}
