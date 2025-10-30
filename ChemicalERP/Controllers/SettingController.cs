using ChemicalERP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using Microsoft.Extensions.Options;
using ChemicalERP.Models.KendoGridManager;
using static Dapper.SqlMapper;

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
            return Ok(grid);   
        }
        [HttpPost("saveBank")]
        public async Task<IActionResult> SaveBank([FromBody] Bas_Bank entity)
        {
            if (entity.BankID > 0)
            {
                entity.SaveOption = 2;
            }
            else
            {
                entity.SaveOption = 1;
            }

            var res=await _service.SaveAsync(entity);

            
            if (res.ErrorNo != 0)
                return StatusCode(500, new { message = res.Message, errorNo = res.ErrorNo });

            if (res.NoofRows <= 0 || res.ResultId <= 0)
                return BadRequest(new { message = res.Message });

            return Ok(new
            {
                message = res.Message ?? "Saved",
                ResultId = res.ResultId,
                rows = res.NoofRows
            });
        }

    }


}
