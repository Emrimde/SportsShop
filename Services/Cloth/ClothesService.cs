using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces.ICloth;

namespace Services
{
    public class ClothGetterService : IClothGetterService
    {
        private readonly IClothRepository _clothRepository;
        private readonly ILogger<ClothGetterService> _logger;

        public ClothGetterService(IClothRepository clothRepository, ILogger<ClothGetterService> logger)
        {
            _clothRepository = clothRepository;
            _logger = logger;
        }

        public async Task<List<ClothResponse>> FilterClothes(string size, string gender, string type)
        {
            _logger.LogDebug("FilterClothes service method. Parameters: size: {size}, gender: {gender}, type: {type}", size, gender, type);

            IQueryable<Cloth> clothes = _clothRepository.FilterClothes(size, gender, type); 

            if (gender != "select")
            {
                clothes = clothes.Where(item => item.Gender == gender);
            }
            if (size != "select")
            {
                clothes = clothes.Where(item => item.Size == size);
            }
            if (type != "select")
            {
                clothes = clothes.Where(item => item.Type == type);
            }
            List<ClothResponse> clothResponses = await clothes.Select(item => item.ToClothResponse()).ToListAsync();

            return clothResponses;
        }

        public List<ClothResponse> GetAllClothes()
        {
            _logger.LogDebug("GetAllClothes service method");

            return _clothRepository.GetAllClothes().Select(item => item.ToClothResponse()).ToList();
        }

        public async Task<ClothResponse?> GetClothById(int id)
        {
            _logger.LogDebug("GetClothById service method. Parameter: id: {id}", id);

            Cloth? cloth = await _clothRepository.GetClothById(id);
            if (cloth == null)
                return null;
            return cloth.ToClothResponse();
        }
    }
}
