using EasyAcme.Model.ViewModels;

namespace EasyAcme.Logic;

public interface IAcmeOrderService
{
    Task<List<AcmeOrderViewModel>> GetAcmeOrdersAsync();
    Task CreateAcmeOrderAsync(CreateAcmeOrderModel acmeOrderModel);
    Task DeleteAcmeOrderAsync(int orderId);
}