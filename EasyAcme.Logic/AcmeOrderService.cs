using EasyAcme.DataAccess;
using EasyAcme.Model;
using EasyAcme.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EasyAcme.Logic;

public class AcmeOrderService : IAcmeOrderService
{
    private readonly ILogger<AcmeOrderService> _logger;
    private readonly ApplicationContext _applicationContext;

    public AcmeOrderService(ILogger<AcmeOrderService> logger, ApplicationContext applicationContext)
    {
        _logger = logger;
        _applicationContext = applicationContext;
    }

    public Task<List<AcmeOrderViewModel>> GetAcmeOrdersAsync()
    {
        return _applicationContext.AcmeOrders.Select(o => new AcmeOrderViewModel
        {
            Id = o.Id,
            CommonName = o.CommonName,
            AcmeAccountId = o.AcmeAccountId,
        }).ToListAsync();
    }

    public Task<bool> CreateAcmeOrderAsync(CreateAcmeOrderModel acmeOrderModel)
    {
        ArgumentNullException.ThrowIfNull(acmeOrderModel);

        return ExecuteAsync();

        async Task<bool> ExecuteAsync()
        {
            var domainNames = acmeOrderModel.AdditionalDomainNames.DistinctBy(d => d.HostName);
            if (acmeOrderModel.AuthorizationChallengeType != AuthorizationChallengeType.Dns)
            {
                domainNames = domainNames.Where(d => !d.IsWildcard);
            }

            var acmeOrder = new AcmeOrder
            {
                CommonName = acmeOrderModel.CommonName,
                AcmeAccountId = acmeOrderModel.AcmeAccountId!.Value,
                AdditionalDomainNames = string.Join(";", domainNames.Select(x => x.HostName)),
                AuthorizationChallengeType = acmeOrderModel.AuthorizationChallengeType,
                CountryName = acmeOrderModel.CountryName,
                State = acmeOrderModel.State,
                Locality = acmeOrderModel.Locality,
                Organization = acmeOrderModel.Organization,
                DnsChallengePlugin = acmeOrderModel.DnsChallengePlugin,
                DnsChallengeSettings = acmeOrderModel.DnsChallengeSettings,
                OrganizationUnit = acmeOrderModel.OrganizationUnit
            };

            await _applicationContext.AcmeOrders.AddAsync(acmeOrder);
            await _applicationContext.SaveChangesAsync();
            return true;
        }
    }

    public async Task<bool> DeleteAcmeOrderAsync(int orderId)
    {
        var order = await _applicationContext.AcmeOrders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
            return true;

        _applicationContext.AcmeOrders.Remove(order);
        await _applicationContext.SaveChangesAsync();
        return true;
    }
}