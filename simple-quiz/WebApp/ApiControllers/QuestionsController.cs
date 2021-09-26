using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        public QuestionsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Questions
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Question>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Question>>> GetQuestions()
        {
            var all = await _bll.Questions.GetAllAsync();
            var res = new List<PublicApi.DTO.v1.Question>();

            foreach (var item in all)
            {
                res.Add(_mapper.Map<Question, PublicApi.DTO.v1.Question>(item!));
            }

            return Ok(res);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Question>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Question>> GetQuestion(Guid id)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(id);

            if (question == default)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<Question, PublicApi.DTO.v1.Question>(question!));
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutQuestion(Guid id, PublicApi.DTO.v1.Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }
            
            var item = _mapper.Map<PublicApi.DTO.v1.Question, Question>(question!);
            _bll.Questions.Update(item);
            await _bll.SaveChangesAsync();
            
            return Ok();
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Question>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicApi.DTO.v1.Question>> PostQuestion(PublicApi.DTO.v1.Question question)
        {
            var bll = _mapper.Map<PublicApi.DTO.v1.Question, Question>(question);
            
            var res = _bll.Questions.Add(bll);
            await _bll.SaveChangesAsync();

            question.Id = res.Id;
            return CreatedAtAction("GetQuestion", new
            {
                id = res.Id,
                version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
            }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(id);
            
            if (question == null)
            {
                return NotFound();
            }

            _bll.Questions.Remove(question!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> QuestionExists(Guid id)
        {
            return await _bll.Questions.ExistsAsync(id);
        }
    }
}
