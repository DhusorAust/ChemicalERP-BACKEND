using ChemicalERP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChemicalERP.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        public LoginController(ILoginService service) => _service = service;

        // GET api/login/getloginuser/{username}/{password}
        [HttpGet("getloginuser/{username}/{password}")]
        public async Task<IActionResult> GetLoginUserInfo(string username, string password)
        {
            var user = await _service.GetLoginUserInfo(username, password);

            if (user == null)
            {
                // Business fail, but transport OK — keep it 200 with a clear payload
                return Ok(new { isCompleted = false, message = "User Not Found!" });
            }

            if (user.IsAuthorizedLogin == 0)
            {
                return Ok(new { isCompleted = false, message = "Access Denied! You are not authorized to login." });
            }

            // Success
            return Ok(new { isCompleted = true, user });
        }
         

        [HttpGet("GetMenuLoad")]
        public async Task<ActionResult<List<Menu>>> GetMenuLoad(int userId)
        {
            List<Menu> ListObjMenu = new List<Menu>(); 
            ListObjMenu = _service.GetMenuLoad(userId);
            return ListObjMenu;
        }

    }


}
