using AutoMapper;
using Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class QuestionMapper: BaseMapper<DTO.Question, DAL.App.DTO.Question>,
        IBaseMapper<BLL.App.DTO.Question, DAL.App.DTO.Question>
    {
        public QuestionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}