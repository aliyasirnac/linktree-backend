using Application.Features.Apps.Constants;
using Application.Features.Apps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Apps.Constants.AppsOperationClaims;

namespace Application.Features.Apps.Queries.GetById;

public class GetByIdAppQuery : IRequest<GetByIdAppResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAppQueryHandler : IRequestHandler<GetByIdAppQuery, GetByIdAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _appRepository;
        private readonly AppBusinessRules _appBusinessRules;

        public GetByIdAppQueryHandler(IMapper mapper, IAppRepository appRepository, AppBusinessRules appBusinessRules)
        {
            _mapper = mapper;
            _appRepository = appRepository;
            _appBusinessRules = appBusinessRules;
        }

        public async Task<GetByIdAppResponse> Handle(GetByIdAppQuery request, CancellationToken cancellationToken)
        {
            App? app = await _appRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appBusinessRules.AppShouldExistWhenSelected(app);

            GetByIdAppResponse response = _mapper.Map<GetByIdAppResponse>(app);
            return response;
        }
    }
}
