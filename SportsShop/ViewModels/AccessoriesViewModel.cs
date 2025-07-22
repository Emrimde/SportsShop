using SportsShop.Core.ServiceContracts.DTO.GymnasticRingDto;
using SportsShop.Core.ServiceContracts.DTO.TrainingRubberDto;
using SportsShop.Core.ServiceContracts.DTO.WeightPlateDto;
using SportsShop.Core.ServiceContracts.Enums;

namespace SportsShop.UI.ViewModels;
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
