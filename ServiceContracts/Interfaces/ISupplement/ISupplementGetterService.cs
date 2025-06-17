using ServiceContracts.DTO.SupplementDto;

namespace ServiceContracts.Interfaces.ISupplement
{
    public interface ISupplementGetterService
    {
        Task<List<SupplementResponse>> GetAllSupplements();
        Task<SupplementResponse> GetSupplementById(int id);
        Task<List<SupplementResponse>> FilterSupplements(string type, string flavor);
    }
}
