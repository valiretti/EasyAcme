using EasyAcme.Model.ViewModels;

namespace EasyAcme.Logic;

public interface IAcmeAccountService
{
    Task<List<AcmeAccountViewModel>> GetAcmeAccountsAsync();
    Task<bool> CreateAcmeAccountsAsync(CreateAcmeAccountModel acmeAccountModel);
    Task<bool> DeactivateAndDeleteAcmeAccountAsync(int accountId);
    Task<Uri?> GetTermsOfServiceAsync(string directoryUrl);
}