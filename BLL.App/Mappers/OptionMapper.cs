using AutoMapper;
using Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class OptionMapper: BaseMapper<DTO.Option, DAL.App.DTO.Option>,
        IBaseMapper<BLL.App.DTO.Option, DAL.App.DTO.Option>
    {
        public OptionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}