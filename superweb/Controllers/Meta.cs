using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace superweb.Controllers
{
    [Route("meta")]
    [Authorize]
    public class Meta: Controller
    {
        [HttpGet("identity")]
        public IActionResult GetIdentity()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
