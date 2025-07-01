using Entities.Models;

namespace RepositoryContracts
{
    public interface ITrainingRubberRepository
    {
        IQueryable<TrainingRubber> GetAllTrainingRubbers();
        Task<TrainingRubber?> GetTrainingRubberById(int id);
    }
}
