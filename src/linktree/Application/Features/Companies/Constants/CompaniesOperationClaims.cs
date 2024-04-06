using NArchitecture.Core.Security.Attributes;

namespace Application.Features.Companies.Constants;

[OperationClaimConstants]
public static class CompaniesOperationClaims
{
    private const string _section = "Companies";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string UpdateCompanyImage = $"{_section}.UpdateCompanyImage";
}
