using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class QuizService: BaseEntityService<IAppUnitOfWork, IQuizRepository, DTO.Quiz, DAL.App.DTO.Quiz>,
        IQuizService
    {
        public QuizService(IAppUnitOfWork serviceUow, IQuizRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new QuizMapper(mapper))
        {
        }
    }
}