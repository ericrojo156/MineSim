using MyMicroservice.Models;

namespace MyMicroservice.DataAccess
{
    public interface IMineRepository
    {
        public Models.RubyMine GetMineState();
        public ExcavationResult Excavate();
        public BuyResult BuyEquipment(string equipmentName);
    }
}
