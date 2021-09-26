using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SelectedOptionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        public SelectedOptionsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/SelectedOptions
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Question>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<SelectedOption>>> GetSelectedOptions()
        {
            var all = await _bll.SelectedOptions.GetAllAsync();
            var res = new List<PublicApi.DTO.v1.SelectedOption>();

            foreach (var item in all)
            {
                res.Add(_mapper.Map<SelectedOption, PublicApi.DTO.v1.SelectedOption>(item!));
            }

            return Ok(res);
        }

        // GET: api/SelectedOptions/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.SelectedOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.SelectedOption>> GetSelectedOption(Guid id)
        {
            var selectedOption = await _bll.SelectedOptions.FirstOrDefaultAsync(id);

            if (selectedOption == default)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<SelectedOption, PublicApi.DTO.v1.SelectedOption>(selectedOption!));
        }

        // PUT: api/SelectedOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutSelectedOption(Guid id, PublicApi.DTO.v1.SelectedOption selectedOption)
        {
            if (id != selectedOption.Id)
            {
                return BadRequest();
            }
            
            var item = _mapper.Map<PublicApi.DTO.v1.SelectedOption, SelectedOption>(selectedOption!);
            _bll.SelectedOptions.Update(item);
            await _bll.SaveChangesAsync();
            
            return Ok();
        }

        // POST: api/SelectedOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.SelectedOption>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicApi.DTO.v1.SelectedOption>> PostSelectedOption(PublicApi.DTO.v1.SelectedOption selectedOption)
        {
            var bll = _mapper.Map<PublicApi.DTO.v1.SelectedOption, SelectedOption>(selectedOption);
            
            var res = _bll.SelectedOptions.Add(bll);
            await _bll.SaveChangesAsync();

            selectedOption.Id = res.Id;
            return CreatedAtAction("GetSelectedOption", new
            {
                id = res.Id,
                version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
            }, selectedOption);
        }

        // DELETE: api/SelectedOptions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSelectedOption(Guid id)
        {
            var selectedOption = await _bll.SelectedOptions.FirstOrDefaultAsync(id);
            
            if (selectedOption == null)
            {
                return NotFound();
            }

            _bll.SelectedOptions.Remove(selectedOption!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SelectedOptionExists(Guid id)
        {
            return await _bll.SelectedOptions.ExistsAsync(id);
        }
    }
}
