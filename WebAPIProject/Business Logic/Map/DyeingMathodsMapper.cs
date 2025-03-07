using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Map
{
    public class DyeingMathodsMapper
    {
               
        public DyeingMethod DyeingMathodMapper(DyeMethodSaveRequest request, int userId)
        {
            if (request == null)
            {
                return null;
            }

            return new DyeingMethod
            {
                DyeMethods = request.DyeingMethods?.Trim(),
                DyeMethodCode = request.DyeMethodCode,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow


            };

        }

        public DyeingMethod UpdatedDyeingMapper(DyeingMethod entity, DyeMethodSaveRequest request, int userId)
        {
            entity.DyeMethods = request.DyeingMethods.Trim();
            entity.IsActive = true;
            entity.UpdatedBy = request.Id;
            return entity;
        }


    }
}

