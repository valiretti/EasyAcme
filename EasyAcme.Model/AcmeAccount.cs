using System.ComponentModel.DataAnnotations;
using Certes;

namespace EasyAcme.Model;

public class AcmeAccount
{
    public int Id { get; set; }

    [Required]
    public string ServerIdentifier { get; set; } = null!;
    public string? DisplayName { get; set; }
    public string PemAccountKey { get; set; } = null!;
    public bool AgreementConfirmation { get; set; }
    public string? EabKeyIdentifier { get; set; }
    public string? EabKey { get; set; }
    public KeyAlgorithm? EabKeyAlgorithm { get; set; }

    public ICollection<AcmeAccountEmail> AccountEmails { get; set; } = null!;
}