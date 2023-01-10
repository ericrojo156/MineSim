namespace MyMicroservice.Models
{
    using EquipmentCollection = Dictionary<string, IEquipment>;
    
    public enum ConditionStatus
    {
        Okay,
        Destroyed
    };

    public class Condition {
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
    public class EquipmentBuilder
    {
        public static IEquipment? Build(string name)
        {
            switch (name)
            {
                case "Drill":
                    return new Drill();
                case "Operator":
                    return new Operator();
                default:
                    return null;
            }
        }
    }
    public class Operator : IEquipment
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Condition Condition { get; set; }
        public Operator()
        {
            Name = "Operator";
            Price = 60000;
            Condition = new Condition();
        }
        public Condition UseEquipment(int wearAndTear)
        {
            Condition.Decrement(wearAndTear);
            return Condition;
        }
    }
    public class Drill : IEquipment
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Condition Condition { get; set; }
        public Drill()
        {
            Name = "Drill";
            Price = 1000000;
            Condition = new Condition();
        }
        public Condition UseEquipment(int wearAndTear)
        {
            Condition.Decrement(wearAndTear);
            return Condition;
        }
    }
    public class ExcavationResult
    {
        public string ExcavationMessage { get; set; }

        public int ResourceQuantityObtained { get; set; }

        public List<IEquipment> statusOfUsedEquipment { get; set; }

        public ExcavationResult(int quantityObtained, List<IEquipment> usedEquipment, string message)
        {
            ResourceQuantityObtained = quantityObtained;
            statusOfUsedEquipment = usedEquipment;
            ExcavationMessage = message;
        }
    }
    public class BuyResult
    {
        public string StatusMessage { get; set; }
        public int RemainingMiningProjectBudget { get; set; }
        public IEquipment? EquipmentBought { get; set; }
        public BuyResult()
        {
            StatusMessage = "";
            EquipmentBought = null;
            RemainingMiningProjectBudget = 0;
        }
        public BuyResult(string message, IEquipment boughtEquipment, int remainingBudget)
        {
            StatusMessage = message;
            EquipmentBought = boughtEquipment;
            RemainingMiningProjectBudget = remainingBudget;
        }
    }
    public interface IMineProject
    {
        public int ResourceQuantityRemaining { get; set; }

        public EquipmentCollection EquipmentOnSite { get; set; }

        public int ProjectBudget { get; set; }

        public int LithologyHardness { get; set; }

        public bool AddEquipment(IEquipment newEquipment);

        public ExcavationResult Excavate();

        public BuyResult BuyEquipment(string equipmentName);
    }
    public class RubyMine : IMineProject
    {
        public string MineralType { get; }

        public int ResourceQuantityRemaining { get; set; }

        public EquipmentCollection EquipmentOnSite { get; set; }

        public int ProjectBudget { get; set; }

        public int LithologyHardness { get; set; }

        private string[] requiredEquipment = {
            "Drill",
            "Operator"
        };

        public RubyMine()
        {
            MineralType = "Rubies";
            ProjectBudget = 10000000;
            LithologyHardness = 20;
            ResourceQuantityRemaining = 1000;
            EquipmentOnSite = new EquipmentCollection();
            BuyEquipment("Drill");
            BuyEquipment("Operator");
        }

        public RubyMine(RubyMine mine)
        {
            MineralType = "Rubies";
            ProjectBudget = mine.ProjectBudget;
            LithologyHardness = 20;
            ResourceQuantityRemaining = mine.ResourceQuantityRemaining;
            EquipmentOnSite = mine.EquipmentOnSite;
            BuyEquipment("Drill");
            BuyEquipment("Operator");
        }

        public bool AddEquipment(IEquipment newEquipment)
        {
            bool successfullyBought = false;
            if (ProjectBudget >= newEquipment.Price)
            {
                EquipmentOnSite.Add(newEquipment.Name, newEquipment);
                successfullyBought = true;
            }
            return successfullyBought;
        }

        public List<string> CheckForRequiredExcavationEquipment()
        {
            List<string> missingEquipment = new List<string>();
            foreach (string equipmentName in requiredEquipment)
            {
                IEquipment equipment = EquipmentOnSite.GetValueOrDefault(equipmentName, null);
                if (equipment == null)
                {
                    missingEquipment.Add(equipmentName);
                }
            }
            return missingEquipment;
        }

        public ExcavationResult Excavate()
        {
            EquipmentCollection usedEquipment = new EquipmentCollection();
            List<string> missingEquipmentRequirements = CheckForRequiredExcavationEquipment(); 
            if (missingEquipmentRequirements.Count > 0)
            {
                return new ExcavationResult(
                    0,
                    missingEquipmentRequirements
                        .Select(name => EquipmentBuilder.Build(name))
                        .Where(item => item != null).ToList(),
                    String.Format("Excavation failed because the following equipment items were not available in adequate condition: {0}", String.Join(", ", missingEquipmentRequirements.ToArray()))
                );
            }
            foreach (string equipmentName in requiredEquipment)
            {
                IEquipment equipment = EquipmentOnSite[equipmentName];
                equipment.UseEquipment(LithologyHardness);
                EquipmentOnSite[equipmentName] = equipment;
                if (equipment.Condition.Status == ConditionStatus.Destroyed)
                {
                    EquipmentOnSite.Remove(equipmentName);
                }
                usedEquipment.Add(equipmentName, equipment);
            }
            int minedAmount = LithologyHardness;
            return new ExcavationResult(minedAmount, usedEquipment.Values.ToList(), String.Format("Excavation successfully yielded {0} {1}.", minedAmount, MineralType));
        }
        public BuyResult BuyEquipment(string equipmentName)
        {
            IEquipment equipmentToBuy = EquipmentBuilder.Build(equipmentName);
            if (equipmentToBuy != null && ProjectBudget - equipmentToBuy.Price >= 0)
            {
                ProjectBudget -= equipmentToBuy.Price;
                AddEquipment(equipmentToBuy);
                string successMessage = String.Format("Successfully bought {0} for ${1}", equipmentName, equipmentToBuy.Price);
                return new BuyResult(successMessage, equipmentToBuy, ProjectBudget);
            }
            string failureMessage = String.Format("Failed to buy {0}: cost of equipment is {1}, but the {2} only has a remaining project budget of {3}", equipmentName, equipmentToBuy.Price, MineralType, ProjectBudget);
            return new BuyResult(failureMessage, equipmentToBuy, ProjectBudget);
        }
    }
}
