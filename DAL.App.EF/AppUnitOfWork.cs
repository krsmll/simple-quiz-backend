using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        private IMapper Mapper;

        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IOptionRepository Options => GetRepository(() => new OptionRepository(UowDbContext, Mapper));
        public IQuestionRepository Questions => GetRepository(() => new QuestionRepository(UowDbContext, Mapper));
        public IQuizRepository Quizzes => GetRepository(() => new QuizRepository(UowDbContext, Mapper));

        public ISelectedOptionRepository SelectedOptions =>
            GetRepository(() => new SelectedOptionRepository(UowDbContext, Mapper));
    }
}