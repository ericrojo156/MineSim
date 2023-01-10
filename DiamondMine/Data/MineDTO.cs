using MyMicroservice.Models;

namespace MyMicroservice.Data
{
    public class MineDTO
    {
        public MineDTO(string name, IMineProject mine)
        {
            Name = name;
            RemainingProjectBudget = mine.ProjectBudget;
            EquipmentOnSite = mine.EquipmentOnSite.Values.ToList();
        }

        public string Name { get; set; }

        public int ResourceQuantityRemaining { get; set; }

        public List<IEquipment> EquipmentOnSite { get; set; }

        public int RemainingProjectBudget { get; set; }

    }
}
