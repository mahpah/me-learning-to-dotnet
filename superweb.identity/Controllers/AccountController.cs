using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using superweb.identity.ViewModels;

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
        public IActionResult Login(LoginInputView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToString());
            }

            return Ok();
        }
    }
}
