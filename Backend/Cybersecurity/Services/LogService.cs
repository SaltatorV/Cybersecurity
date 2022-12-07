using AutoMapper;
using Cybersecurity.Entities;
using Cybersecurity.Interfaces.Repositories;
using Cybersecurity.Interfaces.Services;
using Cybersecurity.Models.DTO;

namespace Cybersecurity.Services
{
    public class LogService : ILogService
    {
        private readonly IGenericRepository<Log> _logRepository;
        private readonly IGenericRepository<User> _useRepository;
        private readonly IMapper _mapper;
        
        public LogService(IGenericRepository<Log> logRepository, IGenericRepository<User> useRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _useRepository = useRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LogDto>> GetAllLog()
        {
            var logs = await _logRepository.GetAllAsync();
            var user = await _useRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<LogDto>>(logs);
        }

        public async Task AddLog(string message, string action, int id)
        {
            Log log;

            if (id == 0)
            {
                log = new Log
                {
                    LogTime = DateTime.UtcNow,
                    Action = action,
                    Message = message
                };
            }
            else
            {
                log = new Log
                {
                    LogTime = DateTime.UtcNow,
                    Action = action,
                    UserId = id,
                    Message = message
                };
            }

            await _logRepository.InsertAsync(log);
            await _logRepository.SaveAsync();
        }
    }
}
