using ChemicalERP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using Microsoft.Extensions.Options;
using ChemicalERP.Models.KendoGridManager;

namespace ChemicalERP.Controllers
{
    [Route("api/Setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _service;
        public SettingController(ISettingService service) => _service = service; 
         

        [HttpGet("getBankList/{status}")]
        public async Task<IActionResult> GetBankList([FromRoute] Status status, [FromQuery] string? q = null)
        {
            var grid = await _service.GetBankListAsync(status, q);
            return Ok(grid);   // { Items, TotalCount, Columnses }
        }


    }


}
