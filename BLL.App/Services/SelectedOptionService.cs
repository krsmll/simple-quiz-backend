using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class SelectedOptionService: BaseEntityService<IAppUnitOfWork, ISelectedOptionRepository, DTO.SelectedOption, DAL.App.DTO.SelectedOption>,
        ISelectedOptionService
    {
        public SelectedOptionService(IAppUnitOfWork serviceUow, ISelectedOptionRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new SelectedOptionMapper(mapper))
        {
        }
    }
}