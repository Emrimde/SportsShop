using Entities.Models;
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
            List<ClothResponse> clothResponses = clothes.Select(item => item.ToClothResponse()).ToList();

            return clothResponses;
        }

        public async Task<IEnumerable<ClothResponse>> GetAllClothes()
        {
            IEnumerable<Cloth> clothes = await _clothRepository.GetAllClothes();  
            return clothes.Select(item => item.ToClothResponse());
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
