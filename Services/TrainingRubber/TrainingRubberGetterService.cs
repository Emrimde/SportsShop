using Entities.Models;
using RepositoryContracts;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.Interfaces.ITrainingRubber;

namespace Services
{
    public class TrainingRubberGetterService : ITrainingRubberGetterService
    {
        private readonly ITrainingRubberRepository _trainingRubberRepository;
        public TrainingRubberGetterService(ITrainingRubberRepository trainingRubberRepository)
        {
            _trainingRubberRepository = trainingRubberRepository;
        }

        public async Task<IReadOnlyList<TrainingRubberResponse>> GetAllTrainingRubbers()
        {
            IEnumerable<TrainingRubber> trainingRubbers = await _trainingRubberRepository.GetAllTrainingRubbers();
                
            return trainingRubbers.Select(item => item.ToTrainingRubberResponse()).ToList();
        }

        public async Task<TrainingRubberResponse?> GetTrainingRubberById(int id)
        {
            TrainingRubber? trainingRubber = await _trainingRubberRepository.GetTrainingRubberById(id);

            if (trainingRubber == null)
            {
                return null;
            }

            return trainingRubber.ToTrainingRubberResponse();
        }
    }
}
