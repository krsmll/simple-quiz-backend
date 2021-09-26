using AutoMapper;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, Domain.App.Identity.AppRole>().ReverseMap();
            CreateMap<Option, Domain.App.Option>().ReverseMap();
            CreateMap<Question, Domain.App.Question>().ReverseMap();
            CreateMap<Quiz, Domain.App.Quiz>().ReverseMap();
            CreateMap<SelectedOption, Domain.App.SelectedOption>().ReverseMap();
        }
    }
}