using Microsoft.EntityFrameworkCore;
using WebAPIProject.Business_Logic.Map;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Repositories;

namespace WebAPIProject.Business_Logic.Services
{
    public class DyeTypeService : IDyeTypeService
    {

            private readonly IAPIUserContext _userContext;
            private readonly IDyeTypeRepository _dyeTypeRepository;
            private readonly DyeTypeMapper _mapper;


            public DyeTypeService(IDyeTypeRepository dyeTypeRepository, IAPIUserContext applicationContext, DyeTypeMapper mapper)
            {
            _dyeTypeRepository = dyeTypeRepository;
            _mapper = mapper;
            _userContext = applicationContext;
            }

        public  async Task <DyeTypeDeleteRequest> DeleteDyeType(DyeTypeDeleteRequest request)
        {
          
            var response = new DyeTypeDeleteRequest { Id = request.Id };

            var dyeType = await _dyeTypeRepository.GetById(request.Id);
            if (dyeType == null)
            {
                response.Result = CommonEnum.NotFound;
                return response;
            }

            if (!dyeType.IsActive && !request.IsActive)
            {
                response.Result = CommonEnum.NotFound;
            }

            dyeType.IsActive = request.IsActive;
            await _dyeTypeRepository.UpdateDyeType(dyeType);
            await _dyeTypeRepository.SaveChangesAsync();

            response.IsActive = dyeType.IsActive;
            response.Result = dyeType.IsActive ? CommonEnum.Success : CommonEnum.NotFound;
            return response;
        }
        

      
        public  async Task<DyeTypePaginatedResponse> GetAllDyeType(DyeTypePaginatedRequest request)
        {
            
            var response = new DyeTypePaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var userid = _userContext.UserId;
            var query = _dyeTypeRepository.GetAllActive().Where(x => x.CreatedBy == userid);
 


            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.DyeTypeName.Contains(request.Search));
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
                    .OrderByDescending(x => x.Id)
                    .Skip((request.Index - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DyeTypesaveRequest
                    {
                        Id = x.Id,
                        DyeTypeName = x.DyeTypeName,
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

       

        public  async Task<DyeTypesaveResponse> SaveOrUpdateDyeType(DyeTypesaveRequest request)
        {
            var response = new DyeTypesaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }

            bool isExisting = await _dyeTypeRepository.IsExistingName(request.DyeTypeName.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.Id == 0)
            {
                var newDyeType = _mapper.DyeTypeMap(request,  _userContext.UserId);
                newDyeType.CreatedBy = _userContext.UserId;
                response.Id = await _dyeTypeRepository.Add(newDyeType);
                newDyeType.UpdatedOn = DateTime.UtcNow;
                newDyeType.UpdatedBy = _userContext.UserId;
            }
            else
            {
                //var existingDyeType = await _dyeTypeRepository.GetAllActive()
                //    .FirstOrDefaultAsync(x => x.Id == request.Id);

                var existingDyeType = await _dyeTypeRepository.GetAllActive()
               .FirstOrDefaultAsync(x => x.Id == request.Id) ?? new DyeType();


                if (existingDyeType == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }
                existingDyeType.DyeTypeCode = existingDyeType.DyeTypeCode ?? string.Empty;


                var updatedDyeType = _mapper.UpdatedDyeTypeMapper(existingDyeType, request, _userContext.UserId);
                updatedDyeType.UpdatedBy = _userContext.UserId;
                response.Id = await _dyeTypeRepository.UpdateDyeType(updatedDyeType);
                await _dyeTypeRepository.SaveChangesAsync();
            }

           
            response.Result = response.Id > 0 ? CommonEnum.Success : CommonEnum.Failed;
            return response;
        }

        public async Task<string> GetNextDyeTypeCodeAsync()
        {
            // Get the most recent DyeType
            var maxDyeType = await _dyeTypeRepository.GetMaxDyeTypeAsync();

            if (maxDyeType == null || string.IsNullOrEmpty(maxDyeType.DyeTypeCode))
            {
                // If no valid dye type is found, return "DT0001"
                return "DT0001";
            }

            // Extract the numeric part of the DyeTypeCode and increment it
            var lastCode = maxDyeType.DyeTypeCode.Substring(2);  // Exclude "DT"
            if (int.TryParse(lastCode, out int lastNumber))
            {
                return "DT" + (lastNumber + 1).ToString("D4");
            }
            else
            {
                // If there's an issue with the format of the code, return the default code
                return "DT0001";
            }
        }


    }
}
    
    