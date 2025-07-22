using SportsShop.Core.Domain.Models;

namespace SportsShop.Core.Domain.RepositoryContracts;
public interface ITrainingRubberRepository
{
    Task<IEnumerable<TrainingRubber>> GetAllTrainingRubbers();
    Task<TrainingRubber?> GetTrainingRubberById(int id);
}
