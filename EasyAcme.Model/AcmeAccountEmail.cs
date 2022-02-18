using System.ComponentModel.DataAnnotations;

namespace EasyAcme.Model;

public class AcmeAccountEmail
{
    public int Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    public int AcmeAccountId { get; set; }
    public AcmeAccount AcmeAccount { get; set; } = null!;
}