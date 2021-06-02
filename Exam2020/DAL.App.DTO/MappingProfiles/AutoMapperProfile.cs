

using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Quiz, Domain.App.Quiz>().ReverseMap();
            CreateMap<DAL.App.DTO.Question, Domain.App.Question>().ReverseMap();
            CreateMap<DAL.App.DTO.Result, Domain.App.Result>().ReverseMap();
            CreateMap<DAL.App.DTO.Answer, Domain.App.Answer>().ReverseMap();

        }
    }
}