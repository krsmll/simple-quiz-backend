using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuestionRepository : BaseRepository<DTO.Question, Domain.App.Question, AppDbContext>,
        IQuestionRepository
    {
        private readonly DbContext _context;

        public QuestionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
            new QuestionMapper(mapper))
        {
            _context = dbContext;
        }

        public override async Task<IEnumerable<DTO.Question>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.Quiz)
                .Include(q => q.Options)
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<DTO.Question?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            await Task.CompletedTask;
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.Quiz)
                .Include(q => q.Options)
                .AsEnumerable()
                .Select(x => Mapper.Map(x));
            
            var res = resQuery.FirstOrDefault(e => e!.Id.Equals(id));

            return res!;
        }
    }
}