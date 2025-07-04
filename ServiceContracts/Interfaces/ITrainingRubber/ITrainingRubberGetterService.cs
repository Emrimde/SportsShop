using ServiceContracts.DTO.TrainingRubberDto;

namespace ServiceContracts.Interfaces.ITrainingRubber
{
    public interface ITrainingRubberGetterService
    {
        List<TrainingRubberResponse> GetAllTrainingRubbers();
        Task<TrainingRubberResponse?> GetTrainingRubberById(int id);
    }
}
