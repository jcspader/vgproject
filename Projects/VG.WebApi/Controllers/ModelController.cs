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
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService ModelService)
        {
            _modelService = ModelService;
        }

        // GET: api/<ModelController>
        [HttpGet]
        public async Task<IEnumerable<ModelDto>> Get()
        {
            return await _modelService.GetAllAsync();
        }

        // GET api/<ModelController>/5
        [HttpGet("{id}")]
        public async Task<ModelDto> Get(int id)
        {
            return await _modelService.GetByIdAsync(id);
        }

        // POST api/<ModelController>
        [HttpPost]
        public async Task Post([FromBody] ModelDto value)
        {
            await _modelService.AddAsync(value);
        }

        // PUT api/<ModelController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ModelDto value)
        {
            var result = await _modelService.UpdateAsync(value);

            if (result.IsFailure)
                return BadRequest(result.Failure.message);
            else if (result.Success == false)
                return BadRequest("Failed on update");

            return Ok();
        }

        // DELETE api/<ModelController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Failure.message);
            else if (result.Success == false)
                return BadRequest("Failed on delete");

            return Ok();
        }
    }
}
