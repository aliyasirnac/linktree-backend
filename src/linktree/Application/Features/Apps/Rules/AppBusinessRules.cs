using Application.Features.Apps.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Apps.Rules;

public class AppBusinessRules : BaseBusinessRules
{
    private readonly IAppRepository _appRepository;
    private readonly ILocalizationService _localizationService;

    public AppBusinessRules(IAppRepository appRepository, ILocalizationService localizationService)
    {
        _appRepository = appRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AppsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AppShouldExistWhenSelected(App? app)
    {
        if (app == null)
            await throwBusinessException(AppsBusinessMessages.AppNotExists);
    }

    public async Task AppIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        App? app = await _appRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AppShouldExistWhenSelected(app);
    }
}