using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Features.Apps.Queries.GetById;
using Application.Features.Companies.Constants;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Companies.Constants.CompaniesOperationClaims;

namespace Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyQuery : IRequest<GetByIdCompanyResponse>
{
    public int Id { get; set; }

    public class GetByIdCompanyQueryHandler : IRequestHandler<GetByIdCompanyQuery, GetByIdCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyBusinessRules _companyBusinessRules;

        public GetByIdCompanyQueryHandler(
            IMapper mapper,
            ICompanyRepository companyRepository,
            CompanyBusinessRules companyBusinessRules
        )
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _companyBusinessRules = companyBusinessRules;
        }

        public async Task<GetByIdCompanyResponse> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
        {
            Company? company = await _companyRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                cancellationToken: cancellationToken,
                include: c => c.Include(c => c.Apps)
            );
            await _companyBusinessRules.CompanyShouldExistWhenSelected(company);

            GetByIdCompanyResponse response = _mapper.Map<GetByIdCompanyResponse>(company);
            return response;
        }
    }
}
