using SportsShop.Core.ServiceContracts.DTO.SupplementDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.ISupplement;
public interface ISupplementGetterService
{
    List<SupplementResponse> GetAllSupplements();
    Task<SupplementResponse> GetSupplementById(int id);
    Task<List<SupplementResponse>> FilterSupplements(string type, string flavor);
}
