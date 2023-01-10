using Microsoft.AspNetCore.Mvc;
using MyMicroservice.Data;
using Hxgn.Min.Platform.Libraries;

namespace MyMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController : ControllerBase
{
    private static readonly HttpClient client = new HttpClient();

    [HttpGet(Name = "GetSummary")]
    public async Task<MineSummaries> Get()
    {
        string rubyMineString = await client.GetStringAsync("http://localhost:5006/RubyMine");
        MineDTO rubyMine = Helpers.Serializer.DeserializeJson<MineDTO>(rubyMineString);

        string diamondMineString = await client.GetStringAsync("http://localhost:5007/DiamondMine");
        MineDTO diamondMine = Helpers.Serializer.DeserializeJson<MineDTO>(diamondMineString);

        List<MineDTO> summaries = new List<MineDTO>() { rubyMine, diamondMine }.Where(summary => summary?.Name != null).ToList() ?? new List<MineDTO>();
        MineSummaries mineSummaries = new MineSummaries(summaries);

        return mineSummaries;
    }
}
