using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Map
{
    public class DyeTypeMapper
    {
        public DyeType DyeTypeMap(DyeTypesaveRequest request, int userId)
        {
            if (request == null)
            {
                return null;
            }

            return new DyeType
            {
                DyeTypeName = request.DyeTypeName?.Trim(),
                DyeTypeCode = request.DyeTypeCode
            };
        }

        public DyeType UpdatedDyeTypeMapper(DyeType entity, DyeTypesaveRequest request, int userId)
        {
            entity.DyeTypeName = request.DyeTypeName.Trim();
            entity.IsActive = true;
            return entity;
        }

      
    }
}
