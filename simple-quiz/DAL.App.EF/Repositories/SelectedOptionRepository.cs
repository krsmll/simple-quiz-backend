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
    public class SelectedOptionRepository : BaseRepository<DTO.SelectedOption, Domain.App.SelectedOption, AppDbContext>,
        ISelectedOptionRepository
    {
        private readonly DbContext _context;

        public SelectedOptionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
            new SelectedOptionMapper(mapper))
        {
            _context = dbContext;
        }

        public override async Task<IEnumerable<DTO.SelectedOption>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(s => s.Option)
                    .ThenInclude(o => o!.Question)
                        .ThenInclude(q => q!.Quiz)
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            return res!;
        }
 
        public override async Task<DTO.SelectedOption?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            await Task.CompletedTask;
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(s => s.Option)
                    .ThenInclude(o => o!.Question)
                        .ThenInclude(q => q!.Quiz)
                .AsEnumerable()
                .Select(x => Mapper.Map(x));
            
            var res = resQuery.FirstOrDefault(e => e!.Id.Equals(id));

            return res!;
        }
    }
}