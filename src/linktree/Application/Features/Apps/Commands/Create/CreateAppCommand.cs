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

namespace Application.Features.Apps.Commands.Create;

public class CreateAppCommand
    : IRequest<CreatedAppResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public IFormFile ImageUrl { get; set; }
    public int CompanyId { get; set; }

    public string? PlayStoreUrl { get; set; }
    public string? AppStoreUrl { get; set; }

    public string[] Roles => [Admin, Write, AppsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApps"];

    public class CreateAppCommandHandler : IRequestHandler<CreateAppCommand, CreatedAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppRepository _appRepository;
        private readonly AppBusinessRules _appBusinessRules;
        private readonly ImageServiceBase _imageService;

        public CreateAppCommandHandler(
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

        public async Task<CreatedAppResponse> Handle(CreateAppCommand request, CancellationToken cancellationToken)
        {
            string imageUrl = await _imageService.UploadAsync(request.ImageUrl);

            App app = _mapper.Map<App>(request);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                app.ImageUrl = imageUrl;
            }
            await _appRepository.AddAsync(app);

            CreatedAppResponse response = _mapper.Map<CreatedAppResponse>(app);
            return response;
        }
    }
}
