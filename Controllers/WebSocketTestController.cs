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

    }
}
