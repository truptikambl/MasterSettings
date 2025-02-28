using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Map
{
    public class SymbolCategoryMapper
    {

        public SymbolCategory saventity(SymbolCategoryRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new SymbolCategory
            {
                SymbolCategoryName = request.SymbolCategoryName?.Trim(),
            };
        }

        public SymbolCategory UpdatedSymbolCategoryMapper(SymbolCategory entity, SymbolCategoryRequest request)
        {
            entity.SymbolCategoryName = request.SymbolCategoryName.Trim();
            entity.IsActive= true;
            return entity;
        }


        // IndividualCareSymbol

        public IndividualCareSymbol saventity(IndividualCareSymbolRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new IndividualCareSymbol
            {
                SymbolCode = request.SymbolCode,
                name = request.name?.Trim(),
                ImagePathURL = request.ImagePathURL,
                uniqueId = request.uniqueId,
                imageName = request.imageName,
                Description = request.Description,
                
                CountryId = request.CountryId,
                SymbolCategoryId = request.SymbolCategoryId
            };
        }

        public IndividualCareSymbol UpdatedIndividualCareSymbolMapper(IndividualCareSymbol entity, IndividualCareSymbolRequest request)
        {
            entity.name = request.name.Trim();
            return entity;
        }

      
    }
}
