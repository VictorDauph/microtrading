using microTrading.dto;
using microTrading.Models;
using microTrading.RepositoriesEF;

namespace microTrading.Services
{
    public class ActiveService
    {
        private ActiveRepository _activeRepository;
        public ActiveService(ActiveRepository activeRepository) {
            _activeRepository = activeRepository;
        }

        public Active addActive(CreateActiveDto activeDto)
        {
           return _activeRepository.Add(activeDto);

        }
    }
}
