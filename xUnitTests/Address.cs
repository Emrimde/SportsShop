using ServiceContracts.DTO;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class Address
    {
        private readonly IAddressesService _addressesService;
        public Address(IAddressesService addressesService)
        {
            _addressesService = addressesService;
        }
        public void AddAddress_ProperCountryDetails()
        {
            //Arrange
            AddressDTO address = new AddressDTO
            {
                Id = 1,
                Country = "USA",
                City = "New York",
                Street = "5th Avenue",
                ZipCode = "10001"
            };



        }
    }
}
