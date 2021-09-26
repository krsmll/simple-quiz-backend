using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class OptionMapper : BaseMapper<DAL.App.DTO.Option, Domain.App.Option>,
        IBaseMapper<DAL.App.DTO.Option, Domain.App.Option>
    {
        public OptionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}