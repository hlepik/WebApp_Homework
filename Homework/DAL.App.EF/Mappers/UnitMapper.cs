using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class UnitMapper: BaseMapper<DAL.App.DTO.Unit, Domain.App.Unit>, IBaseMapper<DAL.App.DTO.Unit, Domain.App.Unit>
    {
        public UnitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}