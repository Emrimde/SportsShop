using SportsShop.Core.ServiceContracts.DTO.TrainingRubberDto;

namespace SportsShop.Core.ServiceContracts.Interfaces.ITrainingRubber;
public interface ITrainingRubberGetterService
{
    Task<IReadOnlyList<TrainingRubberResponse>> GetAllTrainingRubbers();
    Task<TrainingRubberResponse?> GetTrainingRubberById(int id);
}
