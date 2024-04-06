using Application.Features.Companies.Constants;
using Application.Features.Companies.Rules;
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
using static Application.Features.Companies.Constants.CompaniesOperationClaims;

namespace Application.Features.Companies.Commands.UpdateCompanyImage;

public class UpdateCompanyImageCommand
    : IRequest<UpdateCompanyImageResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public IFormFile Image { get; set; }

    public string[] Roles => [Admin, Write, CompaniesOperationClaims.UpdateCompanyImage];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCompanies"];

    public class UpdateCompanyImageCommandHandler : IRequestHandler<UpdateCompanyImageCommand, UpdateCompanyImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly ICompanyRepository _companyRepository;
        private readonly ImageServiceBase _imageService;

        public UpdateCompanyImageCommandHandler(
            IMapper mapper,
            CompanyBusinessRules companyBusinessRules,
            ICompanyRepository companyRepository,
            ImageServiceBase imageService
        )
        {
            _mapper = mapper;
            _companyBusinessRules = companyBusinessRules;
            _companyRepository = companyRepository;
            _imageService = imageService;
        }

        public async Task<UpdateCompanyImageResponse> Handle(
            UpdateCompanyImageCommand request,
            CancellationToken cancellationToken
        )
        {
            string imageUrl = "";
            Company? company = await _companyRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _companyBusinessRules.CompanyShouldExistWhenSelected(company);

            if (company?.ImageUrl != null)
                imageUrl = await _imageService.UpdateAsync(request.Image, company.ImageUrl);
            company = _mapper.Map(request, company);
            if (company != null)
            {
                company.ImageUrl = imageUrl;
                await _companyRepository.UpdateAsync(company!);
            }

            UpdateCompanyImageResponse response = _mapper.Map<UpdateCompanyImageResponse>(null);
            return response;
        }
    }
}
