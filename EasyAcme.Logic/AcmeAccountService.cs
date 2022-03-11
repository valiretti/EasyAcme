using Certes;
using EasyAcme.DataAccess;
using EasyAcme.Model;
using EasyAcme.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EasyAcme.Logic;

public class AcmeAccountService : IAcmeAccountService
{
    private readonly ApplicationContext _applicationContext;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<AcmeAccountService> _logger;

    public AcmeAccountService(ApplicationContext applicationContext, IMemoryCache memoryCache, ILogger<AcmeAccountService> logger)
    {
        _applicationContext = applicationContext;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<Uri?> GetTermsOfServiceAsync(string directoryUrl)
    {
        if (string.IsNullOrEmpty(directoryUrl))
            return null;

        return await _memoryCache.GetOrCreateAsync(directoryUrl, async _ =>
        {
            var ctx = new AcmeContext(new Uri(directoryUrl));
            return await ctx.TermsOfService();
        });
    }

    public Task<List<AcmeAccountViewModel>> GetAcmeAccountsAsync()
    {
        return _applicationContext.AcmeAccounts.Select(a => new AcmeAccountViewModel
        {
            Id = a.Id,
            DisplayName = a.DisplayName,
            ServerIdentifier = a.ServerIdentifier,
            AccountEmails = string.Join("; ", a.AccountEmails.Select(e => e.Email))
        }).ToListAsync();
    }

    public Task<bool> CreateAcmeAccountsAsync(CreateAcmeAccountModel acmeAccountModel)
    {
        ArgumentNullException.ThrowIfNull(acmeAccountModel);
        if (acmeAccountModel.AccountEmails == null || acmeAccountModel.AccountEmails.Count < 1)
        {
            throw new ArgumentException("ACME Account must have at least one email address.");
        }

        return ExecuteAsync();

        async Task<bool> ExecuteAsync()
        {
            var context = new AcmeContext(new Uri(acmeAccountModel.ServerIdentifier));
            var pem = context.AccountKey.ToPem();
            var contact = acmeAccountModel.AccountEmails.Select(e => $"mailto:{e}").ToList();
            try
            {
                if (!string.IsNullOrEmpty(acmeAccountModel.EabKeyIdentifier) && !string.IsNullOrEmpty(acmeAccountModel.EabKey))
                {
                    await context.NewAccount(contact, acmeAccountModel.AgreementConfirmation, acmeAccountModel.EabKeyIdentifier,
                        acmeAccountModel.EabKey, acmeAccountModel.EabKeyAlgorithm?.ToString());
                }
                else
                {
                    await context.NewAccount(contact, acmeAccountModel.AgreementConfirmation);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error has occurred during creation the ACME account.");
                return false;
            }

            var acmeAccount = new AcmeAccount
            {
                DisplayName = acmeAccountModel.DisplayName,
                PemAccountKey = pem,
                AgreementConfirmation = acmeAccountModel.AgreementConfirmation,
                ServerIdentifier = acmeAccountModel.ServerIdentifier,
                EabKey = acmeAccountModel.EabKey,
                EabKeyIdentifier = acmeAccountModel.EabKeyIdentifier,
                EabKeyAlgorithm = acmeAccountModel.EabKeyAlgorithm,
                AccountEmails = new List<AcmeAccountEmail>()
            };

            foreach (var accountEmail in acmeAccountModel.AccountEmails)
            {
                acmeAccount.AccountEmails.Add(new AcmeAccountEmail
                {
                    Email = accountEmail
                });
            }

            await _applicationContext.AcmeAccounts.AddAsync(acmeAccount);
            await _applicationContext.SaveChangesAsync();
            return true;
        }
    }

    public async Task<bool> DeactivateAndDeleteAcmeAccountAsync(int accountId)
    {
        var account = await _applicationContext.AcmeAccounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account == null) 
            return true;

        var accountKey = KeyFactory.FromPem(account.PemAccountKey);
        var acme = new AcmeContext(new Uri(account.ServerIdentifier), accountKey);
        try
        {
            var acmeAccount = await acme.Account();
            await acmeAccount.Deactivate();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error has occurred during deactivating the ACME account.");
            return false;
        }

        _applicationContext.AcmeAccounts.Remove(account);
        await _applicationContext.SaveChangesAsync();
        return true;
    }
}