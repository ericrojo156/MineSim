using MyMicroservice.Models;

namespace MyMicroservice.DataAccess
{
    public interface IMineRepository
    {
        public Models.DiamondMine GetMineState();
        public ExcavationResult Excavate();
        public BuyResult BuyEquipment(string equipmentName);
    }
}
