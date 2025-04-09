using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface IAccessoriesService
    {
        Task<List<WeightPlate>> GetAllWeightPlates();
        Task<List<GymnasticRing>> GetAllGymnasticRings();
        Task<List<TrainingRubber>> GetAllTrainingRubbers();
        Task<WeightPlate> GetWeightPlate(int id);
        Task<GymnasticRing> GetGymnasticRing(int id);
        Task<TrainingRubber> GetTrainingRubber(int id);

        Task<dynamic> GetObject(int id);

    }
}
