using System.ComponentModel;

namespace EasyAcme.Model;

public enum AuthorizationChallengeType
{
    [Description("DNS")]
    Dns = 0,

    [Description("HTTP")]
    Http = 1,

    [Description("TLS-ALPN")]
    TlsAlpn = 2
}