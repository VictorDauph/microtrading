using Microsoft.AspNetCore.Mvc;
using microTrading.Services;

//regarder xtb et api xtb developers.xstore.pro/documentation/
namespace microTrading.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XTBLoginController : ControllerBase
    {

        private readonly WebSocketClientService _clientService;

        public XTBLoginController(WebSocketClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("/loginXTB")]
        public void XtbConnection()
        {
            _clientService.LoginToXTBDemoServer();
        }
    }
}
