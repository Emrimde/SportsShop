﻿namespace SportsShop.Core.ServiceContracts.Interfaces.IAddress;
public interface IAddressDeleterService
{
    Task<bool> DeleteAddress(int id, Guid userId);  
}
