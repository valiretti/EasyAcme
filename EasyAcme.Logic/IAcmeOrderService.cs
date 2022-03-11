using EasyAcme.Model.ViewModels;

namespace EasyAcme.Logic;

public interface IAcmeOrderService
{
    Task<List<AcmeOrderViewModel>> GetAcmeOrdersAsync();
    Task<bool> CreateAcmeOrderAsync(CreateAcmeOrderModel acmeOrderModel);
    Task<bool> DeleteAcmeOrderAsync(int orderId);
}