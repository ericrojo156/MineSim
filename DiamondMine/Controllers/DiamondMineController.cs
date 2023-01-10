using Microsoft.AspNetCore.Mvc;
using MyMicroservice.DataAccess;
using MyMicroservice.Data;
using MyMicroservice.Models;

namespace MyMicroservice.Controllers;


[ApiController]
[Route("[controller]")]
public class DiamondMineController : ControllerBase
{
    private static IMineRepository? diamondMineRepository;

    public DiamondMineController(IMineRepository mineRepository)
    {
        diamondMineRepository = mineRepository;
    }

    [HttpGet(Name = "GetDiamonds")]
    public MineDTO Get()
    {
        IMineProject mine = diamondMineRepository?.GetMineState() ?? new DiamondMine();
        return new MineDTO("Diamond Mine", mine);
    }

    [HttpPost(Name = "PostDiamonds")]
    public ExcavationResult Post()
    {
        ExcavationResult extractionResult = diamondMineRepository?.Excavate();
        return extractionResult ?? new ExcavationResult(0, new List<IEquipment>(), "");
    }
}
