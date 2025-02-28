using WebAPIProject.Core.Models;
using static WebAPIProject.Core.DTOs.Constructiondto;
using static WebAPIProject.Core.DTOs.FabricDetails;

namespace WebAPIProject.Business_Logic.Map
{
    public class FabricMapper
    {

        public Fabricc saventity(FabricRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new Fabricc
            {
                FabricType = request.FabricType?.Trim(),
            };
        }

        public Fabricc UpdatedEntity(Fabricc entity, FabricRequest request)
        {
            entity.FabricType = request.FabricType?.Trim();
            return entity;
        }
    
     public Construction saventiti(ConstructionRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new Construction
            {
                ConstructionId = request.ConstructionId,
                ConstructionType = request.ConstructionType?.Trim(),
                FabricId=request.FabricId
            };
        }

        public Construction UpdatedEntiti(Construction entity, ConstructionRequest request)
        {
            if (entity == null || request == null)
                return entity;

            entity.ConstructionType = request.ConstructionType?.Trim();
            entity.ConstructionId = request.ConstructionId;
                entity.FabricId = request.FabricId;
            return entity;
        }
    }
}
