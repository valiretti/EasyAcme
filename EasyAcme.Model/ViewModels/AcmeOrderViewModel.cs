using Certes.Acme.Resource;

namespace EasyAcme.Model.ViewModels;

public record AcmeOrderViewModel
{
    public int Id { get; init; }
    public string CommonName { get; init; } = null!;
    public int AcmeAccountId { get; init; }
    public OrderStatus? Status { get; init; }
    public DateTime? CertificateFrom { get; init; }
    public DateTime? CertificateTo { get; init; }
}