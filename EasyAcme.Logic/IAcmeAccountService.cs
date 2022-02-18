using EasyAcme.Model;

namespace EasyAcme.Logic;

public interface IAcmeAccountService
{
    Task<List<AcmeAccountViewModel>> GetAcmeAccountsAsync();
    Task<bool> CreateAcmeAccountsAsync(CreateAcmeAccountModel acmeAccountModel);
    Task<bool> DeactivateAndDeleteAcmeAccount(int accountId);
    Task<Uri?> GetTermsOfServiceAsync(string directoryUrl);
}