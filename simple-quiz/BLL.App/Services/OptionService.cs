using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OptionService : BaseEntityService<IAppUnitOfWork, IOptionRepository, DTO.Option, DAL.App.DTO.Option>,
        IOptionService
    {
        public OptionService(IAppUnitOfWork serviceUow, IOptionRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new OptionMapper(mapper))
        {
        }
    }
}