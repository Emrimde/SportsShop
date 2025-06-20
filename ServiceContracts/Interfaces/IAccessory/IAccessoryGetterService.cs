﻿using Entities.Models;

namespace ServiceContracts.Interfaces.IAccessory
{
    public interface IAccessoryGetterService
    {
        Task<List<WeightPlate>> GetAllWeightPlates();
        Task<List<GymnasticRing>> GetAllGymnasticRings();
        Task<List<TrainingRubber>> GetAllTrainingRubbers();
        Task<WeightPlate> GetWeightPlate(int id);
        Task<GymnasticRing> GetGymnasticRing(int id);
        Task<TrainingRubber> GetTrainingRubber(int id);
        Task<List<dynamic>> FilterAccessory(string type);
        Task<dynamic> GetObject(int id);
    }
}
