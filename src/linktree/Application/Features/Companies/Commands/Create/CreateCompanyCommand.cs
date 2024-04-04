using Application.Features.Companies.Constants;
using Application.Features.Companies.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Application.Features.Companies.Constants.CompaniesOperationClaims;

namespace Application.Features.Companies.Commands.Create;

public class CreateCompanyCommand : IRequest<CreatedCompanyResponse>, ISecuredRequest, ICacheRemoverRequest,
    ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile ImageUrl { get; set; }

    public string[] Roles => [Admin, Write, CompaniesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCompanies"];

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreatedCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly ImageServiceBase _imageService;

        public CreateCompanyCommandHandler(IMapper mapper, ICompanyRepository companyRepository,
            CompanyBusinessRules companyBusinessRules, ImageServiceBase imageService)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _companyBusinessRules = companyBusinessRules;
            _imageService = imageService;
        }

        public async Task<CreatedCompanyResponse> Handle(CreateCompanyCommand request,
            CancellationToken cancellationToken)
        {
            string imageUrl = await _imageService.UploadAsync(request.ImageUrl);

            Company company = _mapper.Map<Company>(request);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                company.ImageUrl = imageUrl; 
            }

            await _companyRepository.AddAsync(company);

            CreatedCompanyResponse response = _mapper.Map<CreatedCompanyResponse>(company);
            return response;
        }
    }
}