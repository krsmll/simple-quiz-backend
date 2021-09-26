using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<Option, DAL.App.DTO.Option>().ReverseMap();
            CreateMap<Question, DAL.App.DTO.Question>().ReverseMap();
            CreateMap<Quiz, DAL.App.DTO.Quiz>().ReverseMap();
            CreateMap<SelectedOption, DAL.App.DTO.SelectedOption>().ReverseMap();
        }
    }
}