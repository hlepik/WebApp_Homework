using AutoMapper;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Booking, DAL.App.DTO.Booking>().ReverseMap();
            CreateMap<Category, DAL.App.DTO.Category>().ReverseMap();
            CreateMap<City, DAL.App.DTO.City>().ReverseMap();
            CreateMap<Condition, DAL.App.DTO.Condition>().ReverseMap();
            CreateMap<County, DAL.App.DTO.County>().ReverseMap();
            CreateMap<Material, DAL.App.DTO.Material>().ReverseMap();
            CreateMap<MessageForm, DAL.App.DTO.MessageForm>().ReverseMap();
            CreateMap<Picture, DAL.App.DTO.Picture>().ReverseMap();
            CreateMap<Product, DAL.App.DTO.Product>().ReverseMap();
            CreateMap<ProductMaterial, DAL.App.DTO.ProductMaterial>().ReverseMap();
            CreateMap<Unit, DAL.App.DTO.Unit>().ReverseMap();
            CreateMap<UserBookedProducts, DAL.App.DTO.UserBookedProducts>().ReverseMap();
            CreateMap<UserMessages, DAL.App.DTO.UserMessages>().ReverseMap();
            CreateMap<Identity.AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<Identity.AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
        }
    }
}