namespace ServiceContracts.Interfaces.IAccessory
{
    public interface IAccessoryGetterService
    {
        Task<List<dynamic>> FilterAccessory(string type);
        Task<dynamic> GetObject(int id);
    }
}
