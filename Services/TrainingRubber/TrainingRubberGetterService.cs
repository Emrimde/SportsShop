using Entities.Models;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.Interfaces.ITrainingRubber;

namespace Services
{
    public class TrainingRubberGetterService : ITrainingRubberGetterService
    {
        private readonly ITrainingRubberRepository _trainingRubberRepository;
        private readonly ILogger<TrainingRubberGetterService> _logger;

        public TrainingRubberGetterService(ITrainingRubberRepository trainingRubberRepository, ILogger<TrainingRubberGetterService> logger)
        {
            _trainingRubberRepository = trainingRubberRepository;
            _logger = logger;
        }

        public List<TrainingRubberResponse> GetAllTrainingRubbers()
        {
            _logger.LogDebug("GetAllTrainingRubbers service method");
            return  _trainingRubberRepository.GetAllTrainingRubbers().Select(item => item.ToTrainingRubberResponse())
                .ToList();
        }

        public async Task<TrainingRubberResponse?> GetTrainingRubberById(int id)
        {
            _logger.LogDebug("GetTrainingRubberById service method. Parameter: id: {id}", id);
            TrainingRubber? trainingRubber = await _trainingRubberRepository.GetTrainingRubberById(id);

            if (trainingRubber == null)
            {
                return null;
            }

            return trainingRubber.ToTrainingRubberResponse();
        }
    }
}
