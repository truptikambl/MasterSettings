using Microsoft.EntityFrameworkCore;
using WebAPIProject.Business_Logic.Map;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.Models;
using static WebAPIProject.Core.DTOs.Constructiondto;
using static WebAPIProject.Core.DTOs.FabricDetails;

namespace WebAPIProject.Business_Logic.Services
{
    public class FabricService : IFabricService
    {
        private readonly IFabricRepository _fabricRepository;
        
        private readonly FabricMapper _mapper;

        public FabricService(IFabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository ?? throw new ArgumentNullException(nameof(fabricRepository));
            _mapper = new FabricMapper();
        }

        public async Task<FabricSaveResponse> SaveOrUpdate(FabricRequest request)
        {
            var response = new FabricSaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }

            bool isExisting = await _fabricRepository.IsExistingName(request.FabricType.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.FabricId == 0)
            {
                var newFabric = _mapper.saventity(request);
                response.FabricId = await _fabricRepository.Add(newFabric);
            }
            else
            {
                var existingFabric = await _fabricRepository.GetAllActive()
                    .FirstOrDefaultAsync(x => x.FabricId == request.FabricId);

                if (existingFabric == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }

                var updatedFabric = _mapper.UpdatedEntity(existingFabric, request);
                response.FabricId = await _fabricRepository.UpdateFabric(updatedFabric);
            }

            await _fabricRepository.SaveChangesAsync();
            response.Result = response.FabricId > 0 ? CommonEnum.Success : CommonEnum.Failed;
            return response;
        }

        public async Task<FabricPaginatedResponse> GetAllFabric(FabricPaginationRequest request)
        {
            var response = new FabricPaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var query = _fabricRepository.GetAllActive();

         
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.FabricType.Contains(request.Search));
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
                    
                    .OrderByDescending(x => x.FabricId)
                    .Skip((request.Index - 1) * request.PageSize) 
                    .Take(request.PageSize)
                    .Select(x => new FabricRequest
                    {
                        FabricId = x.FabricId,
                        FabricType = x.FabricType,
                        //IsActive = x.IsActive,
                        //ConstructionId = x.ConstructionId
                    })
                    .AsNoTracking()
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


        public async Task<FabricDeleteRequest> Delete(FabricDeleteRequest request)
        {
            var response = new FabricDeleteRequest { FabricId = request.FabricId };

            var fabric = await _fabricRepository.GetById(request.FabricId);
            if (fabric == null)
            {
                response.Result = CommonEnum.NotFound;
                return response;
            }

            if (!fabric.IsActive && !request.IsActive)
            {
                response.Result = CommonEnum.NotFound;
            }

            fabric.IsActive = request.IsActive;
            await _fabricRepository.UpdateFabric(fabric);
            await _fabricRepository.SaveChangesAsync();

            response.IsActive = fabric.IsActive;
            response.Result = fabric.IsActive ? CommonEnum.Success: CommonEnum.NotFound;
            return response;
        }




        // construction 


        public async Task<ConstructionSaveResponse> SaveOrUpdate(ConstructionRequest request)
        {
            var response = new ConstructionSaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }

            bool isExisting = await _fabricRepository.IsExistingType(request.ConstructionType.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.ConstructionId == 0)
            {
                var newConstruction = _mapper.saventiti(request);
                response.ConstructionId = await _fabricRepository.Add(newConstruction);

                await _fabricRepository.SaveChangesAsync(); 
                response.Result = response.ConstructionId > 0 ? CommonEnum.Success : CommonEnum.Failed;
                return response; 

            }
            else
            {
                var existingConstruction = await _fabricRepository.GetAllActiveConstruction()
                    .FirstOrDefaultAsync(x => x.ConstructionId == request.ConstructionId);

                if (existingConstruction == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }

                var updatedConstruction = _mapper.UpdatedEntiti(existingConstruction, request);
                response.ConstructionId = await _fabricRepository.UpdateConstruction(updatedConstruction);

               
                await _fabricRepository.SaveChangesAsync();
                response.Result = response.ConstructionId > 0 ? CommonEnum.Success : CommonEnum.Failed;
                return response;
            }
        }
       
        public async Task<ConstructionPaginatedResponse> GetAllActiveConstruction(ConstructionPaginationRequest request)
            {
            var response = new ConstructionPaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var query = _fabricRepository.GetAllActiveConstruction();


            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.ConstructionType.Contains(request.Search));
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
                    .OrderByDescending(x => x.ConstructionId)
                    .Skip((request.Index - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new ConstructionRequest
                    {
                        ConstructionId = x.ConstructionId,
                        ConstructionType = x.ConstructionType ?? "No Construction Type",
                        FabricId=x.FabricId,
                        FabricName= x.Fabricc.FabricType
                        


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

        public async Task<ConstructionDeleteRequest> DeleteConstruction(ConstructionDeleteRequest requestCon)
        {
            var construction = await _fabricRepository.GetAllActiveConstruction()
                .FirstOrDefaultAsync(d => d.ConstructionId == requestCon.Id);

            if (construction == null)
            {
                return new ConstructionDeleteRequest { Id = requestCon.Id, Result = CommonEnum.NotFound };
            }

            construction.IsActive = false;
            await _fabricRepository.UpdateConstruction(construction);
            await _fabricRepository.SaveChangesAsync();

            return new ConstructionDeleteRequest { Id = requestCon.Id, Result = CommonEnum.Success };
        }

      

       
    }
}
