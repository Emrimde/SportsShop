using ServiceContracts.DTO.GymnasticRingDto;
using ServiceContracts.DTO.TrainingRubberDto;
using ServiceContracts.DTO.WeightPlateDto;
using ServiceContracts.Enums;

namespace SportsShop.ViewModels
{
    public class AccessoriesViewModel
    {
        public IReadOnlyList<WeightPlateResponse> WeightPlates { get; set; } = new List<WeightPlateResponse>();
        public IReadOnlyList<GymnasticRingResponse> GymnasticRings { get; set; } = new List<GymnasticRingResponse>();
        public IReadOnlyList<TrainingRubberResponse> TrainingRubbers { get; set;} = new List<TrainingRubberResponse>();
        public List<dynamic>? SpecificAccessories { get; set; } 
        public List<dynamic> MixedAccessories
        {
            get
            {
                var mixedList = new List<dynamic>();
                int maxCount = Math.Max(WeightPlates.Count, Math.Max(GymnasticRings.Count, TrainingRubbers.Count));
                for(int i =0; i< maxCount; i++)
                {
                    if(i < TrainingRubbers.Count)
                    {
                        mixedList.Add(new {Type = AccessoryTypeEnum.TrainingRubber, Data = TrainingRubbers[i]});
                    }
                    if (i < GymnasticRings.Count)
                        mixedList.Add(new { Type = AccessoryTypeEnum.GymnasticRing, Data = GymnasticRings[i] });

                    if (i < WeightPlates.Count)
                        mixedList.Add(new { Type = AccessoryTypeEnum.WeightPlate, Data = WeightPlates[i] });
                }
                return mixedList;
            }
        }
        
    }
}
