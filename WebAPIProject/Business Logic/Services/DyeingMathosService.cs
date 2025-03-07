using Microsoft.EntityFrameworkCore;
using WebAPIProject.Business_Logic.Map;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Services
{
    public class DyeingMathodsService : IDyeingMathodsService
    {

        private readonly IAPIUserContext _userContext;
        private readonly IDyeingMethods dyeingMethods;
        private readonly DyeingMathodsMapper _mapper;


        public DyeingMathodsService(IDyeingMethods dyeingMethods, IAPIUserContext applicationContext, DyeingMathodsMapper mapper)
        {
            this.dyeingMethods = dyeingMethods ?? throw new ArgumentNullException(nameof(dyeingMethods));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }


        public async Task<DyeMethodDeleteRequest> DeleteDyeingMethod(DyeMethodDeleteRequest request)
        {

            var response = new DyeMethodDeleteRequest { Id = request.Id };

            var dyeingMathod = await dyeingMethods.GetById(request.Id);
            if (dyeingMathod == null)
            {
                response.Result = CommonEnum.NotFound;
                return response;
            }

            if (!dyeingMathod.IsActive && !request.IsActive)
            {
                response.Result = CommonEnum.NotFound;
            }

            dyeingMathod.IsActive = request.IsActive;
            await dyeingMethods.UpdateDyeingMathod(dyeingMathod);
            await dyeingMethods.SaveChangesAsync();

            response.IsActive = dyeingMathod.IsActive;
            response.Result = dyeingMathod.IsActive ? CommonEnum.Success : CommonEnum.NotFound;
            return response;
        }

        public async Task<DyeMethodPaginatedResponse> GetAllDyeingMethod(DyeMethodPaginatedRequest request)
        {
            var response = new DyeMethodPaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var userid = _userContext.UserId;
            var query = dyeingMethods.GetAllActive().Where(x => x.CreatedBy == userid);



            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.DyeMethods.Contains(request.Search));
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
                    .Select(x => new DyeMethodSaveRequest
                    {
                        Id = x.Id,
                        DyeingMethods = x.DyeMethods,
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

        

        public async  Task<DyeMethodsaveResponse> SaveOrUpdateDyeingMethod(DyeMethodSaveRequest request)
        {
            var response = new DyeMethodsaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }

            bool isExisting = await dyeingMethods.IsExistingName(request.DyeingMethods.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.Id == 0)
            {
                var newDyeMethod = _mapper.DyeingMathodMapper(request, _userContext.UserId);
                newDyeMethod.CreatedBy = _userContext.UserId;
                response.Id = await dyeingMethods.Add(newDyeMethod);
                newDyeMethod.UpdatedOn = DateTime.UtcNow;
                newDyeMethod.UpdatedBy = _userContext.UserId;
            }
            else
            {

                var existingDyeingMethod = await dyeingMethods.GetAllActive()
               .FirstOrDefaultAsync(x => x.Id == request.Id) ?? new DyeingMethod();


                if (existingDyeingMethod == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }
                existingDyeingMethod.DyeMethodCode = existingDyeingMethod.DyeMethodCode ?? string.Empty;


                var updatedDyeingMethod = _mapper.UpdatedDyeingMapper(existingDyeingMethod, request, _userContext.UserId);
                updatedDyeingMethod.UpdatedBy = _userContext.UserId;
                response.Id = await dyeingMethods.UpdateDyeingMathod(updatedDyeingMethod);
                await dyeingMethods.SaveChangesAsync();
            }
            response.Result = response.Id > 0 ? CommonEnum.Success : CommonEnum.Failed;
            return response;
        }

        public async Task<string> GetNextDyeTypeCode()
        {
            // Get the most recent DyeType
            var maxDyeType = await dyeingMethods.GetMaxDyeType();

            if (maxDyeType == null || string.IsNullOrEmpty(maxDyeType.DyeMethodCode))
            {
                // If no valid dye type is found, return "DT0001"
                return "DT0001";
            }

            // Extract the numeric part of the DyeTypeCode and increment it
            var lastCode = maxDyeType.DyeMethodCode.Substring(2);  // Exclude "DT"
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

