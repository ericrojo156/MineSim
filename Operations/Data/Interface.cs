namespace MyMicroservice.Data
{

    public enum ConditionStatus
    {
        Okay,
        Destroyed
    };

    public class Condition
    {
        public int Points { get; set; }
        public ConditionStatus Status { get; set; }
        public Condition()
        {
            Points = 100;
            Status = ConditionStatus.Okay;
        }
        public ConditionStatus Decrement(int payload)
        {
            Points -= payload;
            if (Points <= 0)
            {
                Points = 0;
                Status = ConditionStatus.Destroyed;
            }
            return Status;
        }
    }
    public interface IEquipment
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Condition Condition { get; set; }
        public Condition UseEquipment(int targetHardness);
    }
}
