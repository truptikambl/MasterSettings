using Microsoft.EntityFrameworkCore;
using WebAPIProject.Business_Logic.Map;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Services
{
    public class SymbolCategoryService : ISymbolCategoryService
    {

        private readonly ISymbolCategoryRepository _symbolCategoryRepository;

        private readonly SymbolCategoryMapper _Mapper;

   




        public SymbolCategoryService(ISymbolCategoryRepository symbolCategoryRepository)
        {
            _symbolCategoryRepository = symbolCategoryRepository;
           
            _Mapper = new SymbolCategoryMapper();

         

            
        }
        /// <summary>
        /// GetAllSymbolCategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SymbolCategoryPaginatedResponse> GetAllSymbolCategory(SymbolCategoryPaginatedRequest request)
        {
            var response = new SymbolCategoryPaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var query = _symbolCategoryRepository.GetAllActive();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.SymbolCategoryName.Contains(request.Search));
            }
            try
            {

                response.TotalRecords = await query.CountAsync();

                if (response.TotalRecords <= 0)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }
                response.Data = await query
                    .AsNoTracking()
                    .OrderByDescending(x => x.SymbolCategoryId)
                    .Skip((request.Index - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new SymbolCategoryRequest
                    {
                        SymbolCategoryId = x.SymbolCategoryId,
                        SymbolCategoryName = x.SymbolCategoryName,
                    })
                    .ToListAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            response.PageCount = (response.TotalRecords / request.PageSize) + (response.TotalRecords % request.PageSize > 0 ? 1 : 0);
            response.Result = CommonEnum.Success;

            return response;
        }

        public async Task<SymbolCategorySaveResponse> saveOrUpdate(SymbolCategoryRequest request)
        {
            var response = new SymbolCategorySaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }
            bool isExisting = await _symbolCategoryRepository.IsExistingName(request.SymbolCategoryName.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.SymbolCategoryId == 0)
            {
                var newSymbolCategory = _Mapper.saventity(request);
                response.SymbolCategoryId = await _symbolCategoryRepository.AddsymbolCategory(newSymbolCategory);
            }
            else
            {
                var existingSymbolCategory = await _symbolCategoryRepository.GetAllActive()
                    .FirstOrDefaultAsync(x => x.SymbolCategoryId == request.SymbolCategoryId);

                if (existingSymbolCategory == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }

                var updatedFabric = _Mapper.UpdatedSymbolCategoryMapper(existingSymbolCategory, request);
                response.SymbolCategoryId = await _symbolCategoryRepository.UpdateSymbolCategory(updatedFabric);
            }

            await _symbolCategoryRepository.SaveChangesAsync();
            response.Result = response.SymbolCategoryId > 0 ? CommonEnum.Success : CommonEnum.Failed;
            return response;
        }

        public async Task<SymbolCategoryDeleteRequest> Delete(SymbolCategoryDeleteRequest request)
        {
            var response = new SymbolCategoryDeleteRequest { SymbolCategoryId = request.SymbolCategoryId };

            var symbolCategory = await _symbolCategoryRepository.GetById(request.SymbolCategoryId);
            if (symbolCategory == null)
            {
                response.Result = CommonEnum.NotFound;
                return response;
            }

            if (!symbolCategory.IsActive && !request.IsActive)
            {
                response.Result = CommonEnum.NotFound;
            }

            symbolCategory.IsActive = request.IsActive;
            await _symbolCategoryRepository.UpdateSymbolCategory(symbolCategory);
            await _symbolCategoryRepository.SaveChangesAsync();

            response.IsActive = symbolCategory.IsActive;
            response.Result = symbolCategory.IsActive ? CommonEnum.Success : CommonEnum.NotFound;
            return response;
        }

        // IndividualCareSymbol


        public async Task<IndividualCareSymbolPaginatedResponse> GetAllIndividualCareSymbol(IndividualCareSymbolPaginationRequest request)
        {
            var response = new IndividualCareSymbolPaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var query = _symbolCategoryRepository.GetAllActiveIndividual();

            //if (!string.IsNullOrEmpty(request.Search))
            //{
            //    // query = query.Where(c => c.name.Contains(request.Search));
            //    query = query.Where(c => string.IsNullOrEmpty(request.Search) || c.name.Contains(request.Search));

            //}

            query.Where(x => x.IsActive);
            query = query.WhereIf(!string.IsNullOrEmpty(request.Search),
                      c => c.name.Contains(request.Search));

            try
            {

                response.TotalRecords = await query.CountAsync();

                if (response.TotalRecords <= 0)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }
                response.Data = await query
                    .AsNoTracking()
                    .OrderByDescending(x => x.SymbolCode)
                    .Skip((request.Index - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new IndividualCareSymbolRequest
                    {
                        SymbolCode = x.SymbolCode,
                        name = x.name,
                        ImagePathURL = x.ImagePathURL,
                        uniqueId = x.uniqueId,
                        imageName = x.imageName,
                        Description = x.Description,
                        CountryId = x.CountryId,
                        SymbolCategoryId = x.SymbolCategoryId
                    })
                    .ToListAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            response.PageCount = (response.TotalRecords / request.PageSize) + (response.TotalRecords % request.PageSize > 0 ? 1 : 0);
            response.Result = CommonEnum.Success;

            return response;
        }

        public async Task<IndividualCareSymbolSaveResponse> saveOrUpdates(IndividualCareSymbolRequest request)
        {
            var response = new IndividualCareSymbolSaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }
            bool isExisting = await _symbolCategoryRepository.IsExistingName(request.name.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.SymbolCode == 0)
            {
                var newIndividualCareSymbol = _Mapper.saventity(request);
                response.SymbolCode = await _symbolCategoryRepository.AddIndividual(newIndividualCareSymbol);
            }
            else
            {
                var existingIndividualCareSymbol = await _symbolCategoryRepository.GetAllActiveIndividual()
                    .FirstOrDefaultAsync(x => x.SymbolCategoryId == request.SymbolCode);

                if (existingIndividualCareSymbol == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }

                var UpdateIndividualCareSymbol = _Mapper.UpdatedIndividualCareSymbolMapper(existingIndividualCareSymbol, request);
                response.SymbolCode = await _symbolCategoryRepository.UpdateIndividual(UpdateIndividualCareSymbol);
            }

            await _symbolCategoryRepository.SaveChangesAsync();
            response.Result = response.SymbolCode > 0 ? CommonEnum.Success : CommonEnum.Failed;
            return response;
        }

        public async Task<IndividualCareSymbolDeleteRequest> Delete(IndividualCareSymbolDeleteRequest request)
        {
            var response = new IndividualCareSymbolDeleteRequest { SymbolCode = request.SymbolCode };

            var IndividualCareSymbol = await _symbolCategoryRepository.GetByIdIndividual(request.SymbolCode);

            if (IndividualCareSymbol == null)
            {
                response.Result = CommonEnum.NotFound;
                return response;
            }

            if (!IndividualCareSymbol.IsActive && !request.IsActive)
            {
                response.Result = CommonEnum.AlreadyInUse;
                return response;
            }

            //IndividualCareSymbol.IsActive = request.IsActive;
            IndividualCareSymbol.IsActive = true;
            await _symbolCategoryRepository.UpdateIndividual(IndividualCareSymbol);
            await _symbolCategoryRepository.SaveChangesAsync();

            response.IsActive = IndividualCareSymbol.IsActive;
            //response.Result = IndividualCareSymbol.IsActive ? CommonEnum.Success : CommonEnum.NotFound;
            response.Result = CommonEnum.Success;
            return response;
        }





    }
}
