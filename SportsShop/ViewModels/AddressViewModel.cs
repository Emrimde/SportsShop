namespace SportsShop.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
    }
}
