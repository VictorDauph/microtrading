using Microsoft.AspNetCore.Mvc;
using microTrading.dto;
using microTrading.Models;
using microTrading.Services;

//regarder xtb et api xtb developers.xstore.pro/documentation/
namespace microTrading.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveController : ControllerBase
    {

        private readonly ActiveService _activeService;

        public ActiveController(ActiveService activeService)
        {
            _activeService = activeService;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Active> Get()
        {
            return _activeService.getAllActives();
        }

        [HttpPost]
        [Route("new")]
        public Active AddNewActive(CreateActiveDto activeDto)
        {
            Active activeSaved = _activeService.addActive(activeDto);
            return activeSaved;
        }
    }
}
