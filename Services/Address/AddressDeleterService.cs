﻿using RepositoryContracts;
using ServiceContracts.Interfaces.IAddress;

namespace Services
{
    /// <summary>
    /// Its a service for managing user's addresses. They can input their address, edit it, delete it and show all addresses before order time.
    /// </summary>
    public class AddressDeleterService : IAddressDeleterService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressDeleterService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> DeleteAddress(int id)
        {
            return await _addressRepository.DeleteAddress(id);
        }
    }
}
