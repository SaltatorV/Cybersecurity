using Cybersecurity.Models.DTO.Log;

namespace Cybersecurity.Interfaces.Services
{
    public interface ILogService
    {
        Task<IEnumerable<LogDto>> GetAllLog();

        Task AddLog(string message, string action, int id);
    }
}
