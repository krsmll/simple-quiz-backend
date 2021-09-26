using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OptionRepository : BaseRepository<DTO.Option, Domain.App.Option, AppDbContext>,
        IOptionRepository
    {
        private readonly DbContext _context;

        public OptionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext,
            new OptionMapper(mapper))
        {
            _context = dbContext;
        }

        public override async Task<IEnumerable<DTO.Option>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(o => o.Question)
                .ThenInclude(q => q!.Quiz)
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<DTO.Option?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            await Task.CompletedTask;
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(o => o.Question)
                    .ThenInclude(q => q!.Quiz)
                .AsEnumerable()
                .Select(x => Mapper.Map(x));
            
            var res = resQuery.FirstOrDefault(e => e!.Id.Equals(id));

            return res!;
        }
    }
}