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
    public class QuizRepository : BaseRepository<DTO.Quiz, Domain.App.Quiz, AppDbContext>,
        IQuizRepository
    {
        private readonly DbContext _context;

        public QuizRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
            new QuizMapper(mapper))
        {
            _context = dbContext;
        }

        public override async Task<IEnumerable<DTO.Quiz>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                        .ThenInclude(o => o.SelectedOptions)
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<DTO.Quiz?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            await Task.CompletedTask;
            
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                        .ThenInclude(o => o.SelectedOptions)
                .AsEnumerable()
                .Select(x => Mapper.Map(x));
            
            var res = resQuery.FirstOrDefault(e => e!.Id.Equals(id));

            return res!;
        }
    }
}