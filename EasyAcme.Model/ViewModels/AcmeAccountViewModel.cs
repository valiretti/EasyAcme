namespace EasyAcme.Model.ViewModels;

public record AcmeAccountViewModel
{
    public int Id { get; init; }
    public string ServerIdentifier { get; init; } = null!;
    public string? DisplayName { get; init; }
    public string AccountEmails { get; init; } = null!;
}