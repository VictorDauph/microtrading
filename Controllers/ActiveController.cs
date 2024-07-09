using Microsoft.AspNetCore.Mvc;
using microTrading.dto;
using microTrading.Models;
using microTrading.Services;


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

        [HttpGet("/all")]
        public IEnumerable<Active> Get()
        {
            return new List<Active>();
        }

        [HttpPost("/new")]
        public Active AddNewActive(CreateActiveDto activeDto)
        {
            Active activeSaved = _activeService.addActive(activeDto);
            return activeSaved;
        }
    }
}
