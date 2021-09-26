using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        public QuizzesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Quizzes
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Quiz>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Quiz>>> GetQuizzes()
        {
            var all = await _bll.Quizzes.GetAllAsync();
            var res = new List<PublicApi.DTO.v1.Quiz>();

            foreach (var item in all)
            {
                res.Add(_mapper.Map<Quiz, PublicApi.DTO.v1.Quiz>(item!));
            }

            return Ok(res);
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Quiz>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id);

            if (quiz == default)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<Quiz, PublicApi.DTO.v1.Quiz>(quiz!));
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutQuiz(Guid id, PublicApi.DTO.v1.Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }
            
            var item = _mapper.Map<PublicApi.DTO.v1.Quiz, Quiz>(quiz!);
            _bll.Quizzes.Update(item);
            await _bll.SaveChangesAsync();
            
            return Ok();
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Quiz>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicApi.DTO.v1.Quiz>> PostQuiz(PublicApi.DTO.v1.Quiz quiz)
        {
            var bll = _mapper.Map<PublicApi.DTO.v1.Quiz, Quiz>(quiz);
            
            var res = _bll.Quizzes.Add(bll);
            await _bll.SaveChangesAsync();

            quiz.Id = res.Id;
            return CreatedAtAction("GetQuiz", new
            {
                id = res.Id,
                version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
            }, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id);
            
            if (quiz == null)
            {
                return NotFound();
            }

            _bll.Quizzes.Remove(quiz!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> QuizExists(Guid id)
        {
            return await _bll.Quizzes.ExistsAsync(id);
        }
    }
}
