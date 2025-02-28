using Microsoft.EntityFrameworkCore;
using WebAPIProject.Business_Logic.Map;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Services
{
    public class DyeStuffService : IDyeStuffService
    {
        //private readonly IDyeStuffRepository _dyeStuffRepository;

        private readonly IDyeStuffRepository _repository;

        //private readonly DyeStuffMapper _Mapper ;
        private readonly DyeStuffMapper dyeStuffMapper;

        //private readonly IAPIUserContext _userContext;
        private readonly IAPIUserContext aPIUserContext;

        public DyeStuffService(IDyeStuffRepository DyeStuffRepository, IAPIUserContext context)
        {
            _repository = DyeStuffRepository;

            dyeStuffMapper = new DyeStuffMapper();

            aPIUserContext = context;
        }


     
        public async Task<DyeStuffPaginatedResponse> GetAllDyeStuff(DyeStuffPaginatedRequest request)
        {
          
            var response = new DyeStuffPaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var userid = aPIUserContext.UserId;
            var query = _repository.GetAllActiveDyeStuff().Where(x=>x.CreateBy==userid);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.DyeStuffName.Contains(request.Search));
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
                    .Select(x => new DyeStuffRequest
                    {
                        Id = x.Id,
                        DyeStuffName = x.DyeStuffName,
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

        

        public async  Task<DyeStuffSaveResponse> saveOrUpdatesDyeStuff(DyeStuffRequest request)
        {

            var response = new DyeStuffSaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Failed;
                return response;
            }
           
            bool isExisting = await _repository.IsExistingNameDyeStuff(request.DyeStuffName.Trim());
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlReadyExist;
                return response;
            }
            if (request.Id == 0)
            {
                var newDyeStuff = dyeStuffMapper.saveDyeStuffMapper(request, aPIUserContext.UserId);
                response.Id = await _repository.AddDyeStuff(newDyeStuff);
            }
            else
            {
                var existingDyeStuff = await _repository.GetAllActiveDyeStuff()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (existingDyeStuff == null)
                {
                    response.Result = CommonEnum.NotFound;
                    return response;
                }

                var updatedDyeStuff = dyeStuffMapper.UpdatedDyeStuffMapper(existingDyeStuff, request,aPIUserContext.UserId);
                response.Id = await _repository.UpdateDyeStuff(updatedDyeStuff);
            }

            await _repository.SaveChangesAsync();
            response.Result = response.Id > 0 ? CommonEnum.Success : CommonEnum.Failed;
            return response;
        }

      
        public async Task<DyeStuffDeleteRequest> Delete(DyeStuffDeleteRequest request)
        {
            var response = new DyeStuffDeleteRequest { Id = request.Id };

            var DyeStuff = await _repository.GetByIdDyeStuff(request.Id);
            if (DyeStuff == null)
            {
                response.Result = CommonEnum.NotFound;
                return response;
            }

            if (!DyeStuff.IsActive && !request.IsActive)
            {
                response.Result = CommonEnum.NotFound;
            }

            DyeStuff.IsActive = request.IsActive;
            await _repository.UpdateDyeStuff(DyeStuff);
            await _repository.SaveChangesAsync();

            response.IsActive = DyeStuff.IsActive;
            response.Result = DyeStuff.IsActive ? CommonEnum.Success : CommonEnum.NotFound;
            return response;
        }
       
    }
    
}



