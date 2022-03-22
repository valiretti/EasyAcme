using System.ComponentModel.DataAnnotations;

namespace EasyAcme.Model;

public class AcmeOrder
{
    public int Id { get; set; }
    public AuthorizationChallengeType AuthorizationChallengeType { get; set; }
    [Required] public string CommonName { get; set; } = null!;

    public int AcmeAccountId { get; set; }
    public AcmeAccount AcmeAccount { get; set; } = null!;

    public string? AdditionalDomainNames { get; set; }

    public string? CountryCode { get; set; }
    public Country? Country { get; set; }

    public string? State { get; set; }
    public string? Locality { get; set; }
    public string? Organization { get; set; }
    public string? OrganizationUnit { get; set; }

    public string? DnsChallengePlugin { get; set; }
    public string? DnsChallengeSettings { get; set; }
}