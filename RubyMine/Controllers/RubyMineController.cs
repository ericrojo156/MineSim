using Microsoft.AspNetCore.Mvc;
using MyMicroservice.DataAccess;
using MyMicroservice.Data;
using MyMicroservice.Models;

namespace MyMicroservice.Controllers;


[ApiController]
[Route("[controller]")]
public class RubyMineController : ControllerBase
{
    private static IMineRepository? rubyMineRepository;

    public RubyMineController(IMineRepository mineRepository)
    {
        rubyMineRepository = mineRepository;
    }

    [HttpGet(Name = "GetRubies")]
    public MineDTO Get()
    {
        int quantity = rubyMineRepository?.GetMineState()?.ResourceQuantityRemaining ?? 0;
        return new MineDTO("Ruby Mine", quantity);
    }

    [HttpPost(Name = "PostRubies")]
    public ExcavationResult Post()
    {
        ExcavationResult extractionResult = rubyMineRepository?.Excavate();
        return extractionResult ?? new ExcavationResult(0, new List<IEquipment>(), "");
    }

}
