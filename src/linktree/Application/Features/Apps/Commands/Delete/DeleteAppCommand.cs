using Application.Features.Apps.Constants;
using Application.Features.Apps.Constants;
using Application.Features.Apps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Apps.Constants.AppsOperationClaims;

namespace Application.Features.Apps.Commands.Delete;

public class DeleteAppCommand
    : IRequest<DeletedAppResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, AppsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApps"];

    public class DeleteAppCommandHandler : IRequestHandler<DeleteAppCommand, DeletedAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _appRepository;
        private readonly AppBusinessRules _appBusinessRules;

        public DeleteAppCommandHandler(IMapper mapper, IAppRepository appRepository, AppBusinessRules appBusinessRules)
        {
            _mapper = mapper;
            _appRepository = appRepository;
            _appBusinessRules = appBusinessRules;
        }

        public async Task<DeletedAppResponse> Handle(DeleteAppCommand request, CancellationToken cancellationToken)
        {
            App? app = await _appRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appBusinessRules.AppShouldExistWhenSelected(app);

            await _appRepository.DeleteAsync(app!);

            DeletedAppResponse response = _mapper.Map<DeletedAppResponse>(app);
            return response;
        }
    }
}
