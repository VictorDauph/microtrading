using microTrading.dto;
using microTrading.Models;
using microTrading.RepositoriesEF;

namespace microTrading.Services
{
    public class ActiveService
    {
        private readonly ActiveRepository _activeRepository;
        private readonly WebSocketClientService _websocketClientService;
        public ActiveService(ActiveRepository activeRepository, WebSocketClientService webSocketClientService) {
            _activeRepository = activeRepository;
            _websocketClientService = webSocketClientService;
        }

        public Active addActive(ActiveDto activeDto)
        {
            return _activeRepository.AddSingleFromDto(activeDto);
        }

        public IEnumerable<Active> getAllActives()
        {
            return _activeRepository.GetAll();
        }

        public async Task<string> getChartLastRequest(GetChartLastRequestDto dto) {
           return await _websocketClientService.getChartLast(dto);
        }
    }
}
