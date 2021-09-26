using AutoMapper;
using Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class SelectedOptionMapper : BaseMapper<DTO.SelectedOption, DAL.App.DTO.SelectedOption>,
        IBaseMapper<BLL.App.DTO.SelectedOption, DAL.App.DTO.SelectedOption>
    {
        public SelectedOptionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}