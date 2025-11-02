using ChemicalERP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChemicalERP.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChemicalERP.Controllers
{
    [Route("api/DropDownList")]
    [ApiController]
    public class DropDownListController : ControllerBase
    {
        private readonly IDropDownListService _service;
        public DropDownListController(IDropDownListService service) => _service = service;
         
        [HttpGet("projects")]
        public async Task<ActionResult<List<DropDownList>>> Getprojects()
        {
            List<DropDownList> list = new List<DropDownList>();
            list = _service.Getprojects(); 
            return list;
        }

        [HttpGet("stores")]
        public async Task<ActionResult<List<DropDownList>>> Getstores()
        {
            List<DropDownList> list = new List<DropDownList>();
            list = _service.Getstores();
            return list;
        }


        [HttpGet("persons")]
        public async Task<ActionResult<List<DropDownList>>> Getpersons()
        {
            List<DropDownList> list = new List<DropDownList>();
            list = _service.Getpersons();
            return list;
        }

        [HttpGet("sections")]
        public async Task<ActionResult<List<DropDownList>>> Getsections()
        {
            List<DropDownList> list = new List<DropDownList>();
            list = _service.Getsections();
            return list;
        }

        [HttpGet("uoms")]
        public async Task<ActionResult<List<DropDownList>>> Getuoms()
        {
            List<DropDownList> list = new List<DropDownList>();
            list = _service.Getuoms();
            return list;
        }

        [HttpGet("chemicals")]
        public async Task<ActionResult<List<DropDownList>>> Getchemicals()
        {
            List<DropDownList> list = new List<DropDownList>();
            list = _service.Getchemicals();
            return list;
        }

    }


}
