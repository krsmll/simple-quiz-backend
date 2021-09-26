using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class QuestionService: BaseEntityService<IAppUnitOfWork, IQuestionRepository, DTO.Question, DAL.App.DTO.Question>,
        IQuestionService
    {
        public QuestionService(IAppUnitOfWork serviceUow, IQuestionRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new QuestionMapper(mapper))
        {
        }
    }
}