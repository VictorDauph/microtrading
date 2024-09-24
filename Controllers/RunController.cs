using Microsoft.AspNetCore.Mvc;
using microTrading.dto;
using microTrading.Services;

namespace microTrading.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunController : ControllerBase
    {
        RunService _runService;
        public RunController(RunService runService){
            _runService = runService;
        }

        [HttpPost]
        [Route("/launchRun")]
        public void LaunchRun(LaunchRunDto dto)
        {
            _runService.LaunchRun(dto);
        }
       
       
    }
}
