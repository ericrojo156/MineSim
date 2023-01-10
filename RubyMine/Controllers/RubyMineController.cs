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
        IMineProject mine = rubyMineRepository?.GetMineState() ?? new RubyMine();
        return new MineDTO("Ruby Mine", mine);
    }

    [HttpPost(Name = "PostRubies")]
    public ExcavationResult Post()
    {
        ExcavationResult extractionResult = rubyMineRepository?.Excavate();
        return extractionResult ?? new ExcavationResult(0, new List<IEquipment>(), "");
    }
}
