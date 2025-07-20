using ServiceContracts.DTO.TrainingRubberDto;

namespace ServiceContracts.Interfaces.ITrainingRubber
{
    public interface ITrainingRubberGetterService
    {
        Task<IReadOnlyList<TrainingRubberResponse>> GetAllTrainingRubbers();
        Task<TrainingRubberResponse?> GetTrainingRubberById(int id);
    }
}
