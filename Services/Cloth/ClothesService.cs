using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO.ClothDto;
using ServiceContracts.Interfaces.ICloth;

namespace Services
{
    public class ClothGetterService : IClothGetterService
    {
        private readonly IClothRepository _clothRepository;

        public ClothGetterService(IClothRepository clothRepository)
        {
            _clothRepository = clothRepository;
        }

        public async Task<List<ClothResponse>> FilterClothes(string size, string gender, string type)
        {
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
            return _clothRepository.GetAllClothes().Select(item => item.ToClothResponse()).ToList();
        }

        public async Task<ClothResponse?> GetClothById(int id)
        {
            Cloth? cloth = await _clothRepository.GetClothById(id);
            if (cloth == null)
                return null;
            return cloth.ToClothResponse();
        }
    }
}
