
using DALBase = DAL.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> : DALBase.BaseMapper<TLeftObject, TRightObject>
    where TRightObject : class?, new()
    where TLeftObject : class?, new()
    {

    }
}