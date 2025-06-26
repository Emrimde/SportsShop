using ServiceContracts.DTO.TrainingRubberDto;

namespace ServiceContracts.Interfaces.ITrainingRubber
{
    public interface ITrainingRubberGetterService
    {
        Task<List<TrainingRubberResponse>> GetAllTrainingRubbers();
        Task<TrainingRubberResponse?> GetTrainingRubberById(int id);
    }
}
