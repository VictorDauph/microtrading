using Microsoft.AspNetCore.Mvc;
using microTrading.Services;

namespace microTrading.Controllers
{
    [ApiController]
    [Route("/WebSocketTest")]
    public class WebSocketTestController: ControllerBase
    {
        private readonly WebSocketClientService _clientService;

        public WebSocketTestController(WebSocketClientService clientService)
        {
            _clientService = clientService;
        }

        /*
        [HttpGet]
        [Route("/testServer")]
        public void testConnection()
        {
            _clientService.ListenToTestServer();
        }
        */

        [HttpGet]
        [Route("/symbols")]
        public void XtbConnection()
        {
            _clientService.getAllSymbols() ;
        }
        
    }
}
