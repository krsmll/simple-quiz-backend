using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;

        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public IOptionService Options => GetService<IOptionService>(() => new OptionService(Uow, Uow.Options, Mapper));

        public IQuestionService Questions =>
            GetService<IQuestionService>(() => new QuestionService(Uow, Uow.Questions, Mapper));

        public IQuizService Quizzes => GetService<IQuizService>(() => new QuizService(Uow, Uow.Quizzes, Mapper));

        public ISelectedOptionService SelectedOptions =>
            GetService<ISelectedOptionService>(() => new SelectedOptionService(Uow, Uow.SelectedOptions, Mapper));
    }
}