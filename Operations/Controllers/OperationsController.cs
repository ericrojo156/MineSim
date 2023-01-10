using Microsoft.AspNetCore.Mvc;
using MyMicroservice.Data;

namespace MyMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController : ControllerBase
{
    [HttpGet(Name = "GetSummary")]
    public MineSummaries Get()
    {
        // TODO: send request to Ruby Mine microservice
        // TODO: send request to MicroService Mine microservice

        MineSummaries mineSummaries = new MineSummaries();
        return mineSummaries;
    }
}
