namespace EasyAcme.Model.ViewModels;

public class DomainNameModel
{
    public string HostName { get; set; } = null!;
    public bool IsWildcard => HostName.StartsWith("*.");
}