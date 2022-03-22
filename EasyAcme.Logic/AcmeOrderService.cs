using EasyAcme.DataAccess;
using EasyAcme.Model;
using EasyAcme.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EasyAcme.Logic;

public class AcmeOrderService : IAcmeOrderService
{
    private readonly ApplicationContext _applicationContext;

    public AcmeOrderService(ApplicationContext applicationContext)
    {
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

    public Task CreateAcmeOrderAsync(CreateAcmeOrderModel acmeOrderModel)
    {
        ArgumentNullException.ThrowIfNull(acmeOrderModel);

        return ExecuteAsync();

        async Task ExecuteAsync()
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
                CountryCode = acmeOrderModel.CountryCode,
                State = acmeOrderModel.State,
                Locality = acmeOrderModel.Locality,
                Organization = acmeOrderModel.Organization,
                DnsChallengePlugin = acmeOrderModel.DnsChallengePlugin,
                DnsChallengeSettings = acmeOrderModel.DnsChallengeSettings,
                OrganizationUnit = acmeOrderModel.OrganizationUnit
            };

            await _applicationContext.AcmeOrders.AddAsync(acmeOrder);
            await _applicationContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAcmeOrderAsync(int orderId)
    {
        var order = await _applicationContext.AcmeOrders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order != null)
        {
            _applicationContext.AcmeOrders.Remove(order);
            await _applicationContext.SaveChangesAsync();
        }
    }
}