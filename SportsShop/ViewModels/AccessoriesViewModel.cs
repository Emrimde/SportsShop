using SportsShop.Models;
using Entities.Models;

namespace SportsShop.ViewModels
{
    public class AccessoriesViewModel
    {
        
        public List<WeightPlate> WeightPlates { get; set; } = new List<WeightPlate>();
        public List<GymnasticRing> GymnasticRings { get; set; } = new List<GymnasticRing>();
        public List<TrainingRubber> TrainingRubbers { get; set;} = new List<TrainingRubber>();

        public List<dynamic>? SpecificAccessories { get; set; } 
        public List<dynamic> MixedAccessories
        {
            get
            {
                var mixedList = new List<dynamic>();
                int maxCount = Math.Max(WeightPlates.Count, Math.Max(GymnasticRings.Count, TrainingRubbers.Count));
                for(int i =0; i< maxCount; i++)
                {
                    if(i <TrainingRubbers.Count)
                    {
                        mixedList.Add(new {Type = "TrainingRubber", Data = TrainingRubbers[i]});
                    }
                    if (i < GymnasticRings.Count)
                        mixedList.Add(new { Type = "GymnasticRing", Data = GymnasticRings[i] });

                    if (i < WeightPlates.Count)
                        mixedList.Add(new { Type = "WeightPlate", Data = WeightPlates[i] });
                   
                }
                return mixedList;

            }
        }
        
    }
}
