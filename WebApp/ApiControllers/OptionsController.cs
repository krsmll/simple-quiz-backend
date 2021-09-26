using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        public OptionsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Options
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Option>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Option>>> GetOptions()
        {
            var all = await _bll.Options.GetAllAsync();
            var res = new List<PublicApi.DTO.v1.Option>();

            foreach (var item in all)
            {
                res.Add(_mapper.Map<Option, PublicApi.DTO.v1.Option>(item!));
            }

            return Ok(res);
        }

        // GET: api/Options/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Option>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Option>> GetOption(Guid id)
        {
            var option = await _bll.Options.FirstOrDefaultAsync(id);

            if (option == default)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<Option, PublicApi.DTO.v1.Option>(option!));
        }

        // PUT: api/Options/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutOption(Guid id, PublicApi.DTO.v1.Option option)
        {
            if (id != option.Id)
            {
                return BadRequest();
            }
            
            var item = _mapper.Map<PublicApi.DTO.v1.Option, Option>(option!);
            _bll.Options.Update(item);
            await _bll.SaveChangesAsync();
            
            return Ok();
        }

        // POST: api/Options
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Option>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicApi.DTO.v1.Option>> PostOption(PublicApi.DTO.v1.Option option)
        {
            var bll = _mapper.Map<PublicApi.DTO.v1.Option, Option>(option);
            
            var res = _bll.Options.Add(bll);
            await _bll.SaveChangesAsync();

            option.Id = res.Id;
            return CreatedAtAction("GetOption", new
            {
                id = res.Id,
                version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
            }, option);
        }

        // DELETE: api/Options/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid id)
        {
            var option = await _bll.Options.FirstOrDefaultAsync(id);
            
            if (option == null)
            {
                return NotFound();
            }

            _bll.Options.Remove(option!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> OptionExists(Guid id)
        {
            return await _bll.Options.ExistsAsync(id);
        }
    }
}