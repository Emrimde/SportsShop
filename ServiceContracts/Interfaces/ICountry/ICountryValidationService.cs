namespace ServiceContracts.Interfaces.ICountry
{
    public interface ICountryValidationService
    {
        Task<bool> IsCountryValid(int countryId);
    }
}
