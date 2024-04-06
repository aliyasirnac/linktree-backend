using Application.Features.Apps.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Apps.Constants.AppsOperationClaims;

namespace Application.Features.Apps.Queries.GetList;

public class GetListAppQuery : IRequest<GetListResponse<GetListAppListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListApps({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetApps";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAppQueryHandler : IRequestHandler<GetListAppQuery, GetListResponse<GetListAppListItemDto>>
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;

        public GetListAppQueryHandler(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAppListItemDto>> Handle(
            GetListAppQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<App> apps = await _appRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAppListItemDto> response = _mapper.Map<GetListResponse<GetListAppListItemDto>>(apps);
            return response;
        }
    }
}
