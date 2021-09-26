using AutoMapper;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, BLL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, BLL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<Option, BLL.App.DTO.Option>().ReverseMap();
            CreateMap<Question, BLL.App.DTO.Question>().ReverseMap();
            CreateMap<Quiz, BLL.App.DTO.Quiz>().ReverseMap();
            CreateMap<SelectedOption, BLL.App.DTO.SelectedOption>().ReverseMap();
        }
    }
}