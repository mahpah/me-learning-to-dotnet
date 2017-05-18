using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace superweb.identity.Controllers
{
    public class AccountController : Controller
    {
        private ILogger _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login(string redirectUrl)
        {
            var vm = new LoginViewModel()
            {
                redirectUrl = redirectUrl
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return BadRequest();
        }
    }
}
