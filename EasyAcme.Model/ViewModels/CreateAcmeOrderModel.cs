using System.ComponentModel.DataAnnotations;

namespace EasyAcme.Model.ViewModels;

public class CreateAcmeOrderModel
{
    public AuthorizationChallengeType AuthorizationChallengeType { get; set; }

    [Display(Name = "Common Name")]
    public string CommonName { get; set; } = null!;

    public int? AcmeAccountId { get; set; }

    public List<DomainNameModel> AdditionalDomainNames { get; set; } = new();
    public string? CountryCode { get; set; }
    public string? State { get; set; }
    public string? Locality { get; set; }
    public string? Organization { get; set; }
    public string? OrganizationUnit { get; set; }

    [Display(Name = "DNS Challenge Plugin")]
    public string? DnsChallengePlugin { get; set; }
    [Display(Name = "DNS Challenge Settings")]
    public string? DnsChallengeSettings { get; set; }
}