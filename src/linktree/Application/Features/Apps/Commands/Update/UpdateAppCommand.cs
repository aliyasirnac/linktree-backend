using Application.Features.Apps.Constants;
using Application.Features.Apps.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Apps.Constants.AppsOperationClaims;

namespace Application.Features.Apps.Commands.Update;

public class UpdateAppCommand
    : IRequest<UpdatedAppResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IFormFile ImageUrl { get; set; }

    public string? PlayStoreUrl { get; set; }
    public string? AppStoreUrl { get; set; }
    public int CompanyId { get; set; }

    public string[] Roles => [Admin, Write, AppsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApps"];

    public class UpdateAppCommandHandler : IRequestHandler<UpdateAppCommand, UpdatedAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _appRepository;
        private readonly AppBusinessRules _appBusinessRules;
        private readonly ImageServiceBase _imageService;

        public UpdateAppCommandHandler(
            IMapper mapper,
            IAppRepository appRepository,
            AppBusinessRules appBusinessRules,
            ImageServiceBase imageService
        )
        {
            _mapper = mapper;
            _appRepository = appRepository;
            _appBusinessRules = appBusinessRules;
            _imageService = imageService;
        }

        public async Task<UpdatedAppResponse> Handle(UpdateAppCommand request, CancellationToken cancellationToken)
        {
            App? app = await _appRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _appBusinessRules.AppShouldExistWhenSelected(app);
            app = _mapper.Map(request, app);
            if (app?.ImageUrl != null)
                await _imageService.UpdateAsync(request.ImageUrl, app.ImageUrl);

            await _appRepository.UpdateAsync(app!);

            UpdatedAppResponse response = _mapper.Map<UpdatedAppResponse>(app);
            return response;
        }
    }
}
