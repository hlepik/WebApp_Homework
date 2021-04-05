using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CategoryMapper: BaseMapper<DAL.App.DTO.Category, Domain.App.Category>, IBaseMapper<DAL.App.DTO.Category, Domain.App.Category>
    {
        public CategoryMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}