using AutoMapper;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Booking, Domain.App.Booking>().ReverseMap();
            CreateMap<DAL.App.DTO.Category, Domain.App.Category>().ReverseMap();
            CreateMap<DAL.App.DTO.City, Domain.App.City>().ReverseMap();
            CreateMap<DAL.App.DTO.Condition, Domain.App.Condition>().ReverseMap();
            CreateMap<DAL.App.DTO.County, Domain.App.County>().ReverseMap();
            CreateMap<DAL.App.DTO.Material, Domain.App.Material>().ReverseMap();
            CreateMap<DAL.App.DTO.MessageForm, Domain.App.MessageForm>().ReverseMap();
            CreateMap<DAL.App.DTO.Picture, Domain.App.Picture>().ReverseMap();
            CreateMap<DAL.App.DTO.Product, Domain.App.Product>().ReverseMap();
            CreateMap<DAL.App.DTO.ProductMaterial, Domain.App.ProductMaterial>().ReverseMap();
            CreateMap<DAL.App.DTO.ProductPictures, Domain.App.ProductPictures>().ReverseMap();
            CreateMap<DAL.App.DTO.Unit, Domain.App.Unit>().ReverseMap();
            CreateMap<DAL.App.DTO.UserBookedProducts, Domain.App.UserBookedProducts>().ReverseMap();
            CreateMap<DAL.App.DTO.UserMessages, Domain.App.UserMessages>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppUser, AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, AppRole>().ReverseMap();
        }
    }
}