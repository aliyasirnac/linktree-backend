using Application.Features.Apps.Constants;
using Application.Features.Apps.Rules;
using AutoMapper;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Apps.Constants.AppsOperationClaims;

namespace Application.Features.Apps.Commands.UpdateImageApp;

public class UpdateImageAppCommand : IRequest<UpdateImageAppResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{

    public string[] Roles => [Admin, Write, AppsOperationClaims.UpdateImageApp];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApps"];
    
    public class UpdateImageAppCommandHandler : IRequestHandler<UpdateImageAppCommand, UpdateImageAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly AppBusinessRules _appBusinessRules;

        public UpdateImageAppCommandHandler(IMapper mapper, AppBusinessRules appBusinessRules)
        {
            _mapper = mapper;
            _appBusinessRules = appBusinessRules;
        }

        public async Task<UpdateImageAppResponse> Handle(UpdateImageAppCommand request, CancellationToken cancellationToken)
        {
            UpdateImageAppResponse response = _mapper.Map<UpdateImageAppResponse>(null);
            return response;
        }
    }
}
