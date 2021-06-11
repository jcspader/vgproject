using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VG.Domain.Dto;
using VG.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VG.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        // GET: api/<TruckController>
        [HttpGet]
        public async Task<IEnumerable<TruckDto>> Get()
        {
            return await _truckService.GetAllAsync();
        }

        // GET api/<TruckController>/5
        [HttpGet("{id}")]
        public async Task<TruckDto> Get(int id)
        {
            return await _truckService.GetByIdAsync(id);
        }

        // POST api/<TruckController>
        [HttpPost]
        public async Task Post([FromBody] TruckDto value)
        {
            await _truckService.AddAsync(value);
        }

        // PUT api/<TruckController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TruckDto value)
        {
            var result = await _truckService.UpdateAsync(value);

            if (result.IsFailure)
                return BadRequest(result.Failure.message);
            else if (result.Success == false)
                return BadRequest("Failed on update");

            return Ok();
        }

        // DELETE api/<TruckController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _truckService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Failure.message);
            else if (result.Success == false)
                return BadRequest("Failed on delete");

            return Ok();
        }
    }
}
