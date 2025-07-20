using Entities.Models;

namespace RepositoryContracts
{
    public interface ITrainingRubberRepository
    {
        Task<IEnumerable<TrainingRubber>> GetAllTrainingRubbers();
        Task<TrainingRubber?> GetTrainingRubberById(int id);
    }
}
