using Cybersecurity.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cybersecurity.Controllers
{
    [Route("api/log")]
    [ApiController]
    [Authorize]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLog()
        {
            var logs = await _logService.GetAllLog();

            return Ok(logs);
        }
    }
}
