using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Map
{
    public class DyeStuffMapper
    {
        public DyeStuff saveDyeStuffMapper(DyeStuffRequest request,int userId)
        {
            if (request == null)
            {
                return null;
            }

            return new DyeStuff
            {
                
                Id = request.Id,
                CreateBy = userId,
                CreateDate=DateTime.UtcNow,
                DyeStuffName = request.DyeStuffName,
            };
        }

        public DyeStuff UpdatedDyeStuffMapper(DyeStuff entity, DyeStuffRequest request,int userId)
        {
            entity.DyeStuffName = request.DyeStuffName.Trim();
            entity.UpdatedBy = userId;
            entity.UpdatedDate= DateTime.UtcNow;
            return entity;
        }


    }


}
