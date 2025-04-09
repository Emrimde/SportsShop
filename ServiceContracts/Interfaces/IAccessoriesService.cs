using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface IAccessoriesService
    {
        Task<List<WeightPlate>> GetAllWeightPlates();
        Task<List<GymnasticRing>> GetAllGymnasticRings();
        Task<List<TrainingRubber>> GetAllTrainingRubbers();
    }
}
