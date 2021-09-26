using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class SelectedOptionMapper : BaseMapper<DAL.App.DTO.SelectedOption, Domain.App.SelectedOption>,
        IBaseMapper<DAL.App.DTO.SelectedOption, Domain.App.SelectedOption>
    {
        public SelectedOptionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}