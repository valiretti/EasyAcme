using EasyAcme.Model;
using EasyAcme.Model.ViewModels;
using FluentValidation;

namespace EasyAcme.Validators;

public class CreateAcmeOrderValidator : AbstractValidator<CreateAcmeOrderModel>
{
    public CreateAcmeOrderValidator()
    {
        RuleFor(x => x.AcmeAccountId).NotNull();

        RuleFor(x => x.CommonName)
            .Cascade(CascadeMode.Stop).NotEmpty()
            .Must(BeValidHostName).When(x => x.AuthorizationChallengeType != AuthorizationChallengeType.Dns, ApplyConditionTo.CurrentValidator)
            .WithMessage("Valid Host name or IP address is required!")
            .Must(BeValidDns).When(x => x.AuthorizationChallengeType == AuthorizationChallengeType.Dns, ApplyConditionTo.CurrentValidator)
            .WithMessage("Valid Host name is required!");

        RuleFor(x => x.DnsChallengePlugin).NotEmpty()
            .When(x => x.AuthorizationChallengeType == AuthorizationChallengeType.Dns)
            .WithName("DNS Challenge Plugin");
        RuleFor(x => x.DnsChallengeSettings).NotEmpty()
            .When(x => x.AuthorizationChallengeType == AuthorizationChallengeType.Dns)
            .WithName("DNS Challenge Settings");
    }

    public static bool BeValidHostName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        return Uri.CheckHostName(name) != UriHostNameType.Unknown;
    }

    public static bool BeValidDns(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        var dns = name;
        if (name.StartsWith("*."))
        {
            dns = name.Substring(2);
            var dotIndex = dns.IndexOf('.');
            if (dotIndex <= 0 || dotIndex == dns.Length - 1)
            {
                return false;
            }
        }

        return Uri.CheckHostName(dns) == UriHostNameType.Dns;
    }
}