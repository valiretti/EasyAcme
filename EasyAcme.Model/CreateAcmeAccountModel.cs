using System.ComponentModel.DataAnnotations;
using Certes;

namespace EasyAcme.Model;

public class CreateAcmeAccountModel
{
    [Required]
    [Url]
    [Display(Name = "Server Identifier")]
    public string ServerIdentifier { get; set; } = null!;
    
    public string? DisplayName { get; set; }

    [Required]
    [Range(typeof(bool), "true", "true", ErrorMessage = "This form disallows unapproved agreement confirmation.")]
    public bool AgreementConfirmation { get; set; }
    public string? EabKeyIdentifier { get; set; }
    public string? EabKey { get; set; }
    public KeyAlgorithm? EabKeyAlgorithm { get; set; }
    [MinLength(1, ErrorMessage = "ACME Account must have at least one email address.")]
    public List<string> AccountEmails { get; } = new();
}