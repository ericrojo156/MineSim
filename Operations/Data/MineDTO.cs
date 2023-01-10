
namespace MyMicroservice.Data
{
    public class MineDTO
    {
        public string Name { get; set; }

        public int ResourceQuantityRemaining { get; set; }

        public List<IEquipment> EquipmentOnSite { get; set; }

        public int RemainingProjectBudget { get; set; }

    }
    public class MineSummaries
    {
        public List<MineDTO> Summaries { get; set; } = new List<MineDTO>();
        public MineSummaries(List<MineDTO> mines)
        {
            Summaries = mines;
        }
    }
}
