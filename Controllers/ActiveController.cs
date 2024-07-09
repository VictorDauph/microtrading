using Microsoft.AspNetCore.Mvc;
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
        public Active AddNewActive(Active active)
        {
            return _activeService.addActive(active);
        }
    }
}
