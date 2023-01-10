using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMicroservice.Data;
using MyMicroservice.DataAccess;
using MyMicroservice.Models;

namespace MyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTransactionController : ControllerBase
    {
        private static IMineRepository? rubyMineRepository;

        public EquipmentTransactionController(IMineRepository mineRepository)
        {
            rubyMineRepository = mineRepository;
        }

        [HttpPost(Name = "PostBuyEquipment")]
        public BuyResult BuyEquipment(BuyEquipmentRequest payload)
        {
            BuyResult buyResult = payload.EquipmentName != null ? (rubyMineRepository?.BuyEquipment(payload.EquipmentName) ?? new BuyResult()) : new BuyResult();
            return buyResult;
        }
    }
}
