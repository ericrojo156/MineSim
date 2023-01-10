namespace MyMicroservice.Data
{
    public class MineDTO
    {
        public MineDTO(String name, int quantity)
        {
            SiteName = name;
            ResourceQuantity = quantity;
        }
        public String SiteName { get; set; }
        public int ResourceQuantity { get; set; }
    }
}
