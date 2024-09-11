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
        private readonly WebSocketClientService _clientService;

        public ActiveController(ActiveService activeService, WebSocketClientService clientService)
        {
            _activeService = activeService;
            _clientService = clientService;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Active> Get()
        {
            return _activeService.getAllActives();
        }

        [HttpPost]
        [Route("new")]
        public Active AddNewActive(ActiveDto activeDto)
        {
            Active activeSaved = _activeService.addActive(activeDto);
            return activeSaved;
        }

        [HttpGet]
        [Route("/symbols")]
        public async Task<string> XtbConnection()
        {
            return await _clientService.getAllSymbols();

        }

        [HttpPost]
        [Route("getChartLastRequest")]
        public async Task<string> getChartLastrequest(GetChartLastRequestDto dto)
        {
            await _activeService.getChartLastRequest(dto);
            return "ok";
        }
    }
}
