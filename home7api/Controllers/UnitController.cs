using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace home7api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IUnit unitService) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> UnitGetAll()
        {
            return Ok(await unitService.GetAllUnits());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UnitById([FromForm] int id)
        {
            return Ok(await unitService.GetById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewUnit([FromForm] CreateUnitDto createUnitDto)
        {
            await unitService.AddUnit(createUnitDto);
            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await unitService.DeleteUnit(id);
            return Ok();
        }
    }
}
