using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Business_Logic.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyMapper _mapper;


        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _mapper = new CompanyMapper();
        }

        public async Task<CompanySaveResponse> SaveOrUpdate(CompanyPostRequest request)
        {
            CompanySaveResponse response = new CompanySaveResponse();

            if (request == null)
            {
                response.Status = SaveStatus.Failed;
                return response;
            }


            bool isExisting = await _companyRepository.IsExistingName(request.CompanyName.Trim());

            if (isExisting)
            {
                response.Status = SaveStatus.NameAllReadyExist;
                return response;
            }

            if (request.Id == 0)
            {
                var newCompany = _mapper.saveentity(request);
                response.Id = await _companyRepository.Add(newCompany);
            }
            else
            {
                //var existingCompany = await _companyRepository.GetAllActive().Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                var existingCompany = (from c in _companyRepository.GetAllActive()
                                       where c.Id == request.Id
                                       select c).FirstOrDefault(); 


                if (existingCompany == null)
                {
                    response.Status = SaveStatus.NotFound;
                    return response;
                }

                var updatedCompany = _mapper.UpdatedEntity(existingCompany, request);
                response.Id = await _companyRepository.UpdateCompany(updatedCompany);
            }

            await _companyRepository.SaveChangesAsync();
            response.Status = response.Id > 0 ? SaveStatus.Success : SaveStatus.Failed;
            return response;
        }


        public async Task<PaginatedResponse> GetAllCompany(PaginationRequest request)
        {
           
            var response = new PaginatedResponse { PageIndex = request.Index, PageSize = request.PageSize };
            var query = _companyRepository.GetAllActive();


            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.CompanyName.Contains(request.Search));

            }
            try
            {

                response.TotalRecords = await query.CountAsync();

                if (response.TotalRecords <= 0)
                {
                    response.Status = SaveStatus.NotFound;
                    return response;
                }

                response.Data = await query
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((request.Index - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CompanyPostRequest
                {


                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    IsActive = x.IsActive,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department != null ? x.Department.DepartmentName : "No Department"


                })

                        .ToListAsync();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            response.PageCount = (response.TotalRecords / request.PageSize) + (response.TotalRecords % request.PageSize > 0 ? 1 : 0);
            response.Status = SaveStatus.Success;


            return response;
        }

        public async Task<CompanyDeleteRequest> Delete(CompanyDeleteRequest request)
        {
            CompanyDeleteRequest response = new CompanyDeleteRequest { Id = request.Id };


            var company = await _companyRepository.GetById(request.Id);
            if (company == null)
            {
                response.Status = SaveStatus.NotFound;
                return response;
            }

            if (!company.IsActive)
            {
                response.Status = SaveStatus.NotFound;
                return response;
            }
            company.IsActive = request.IsActive;
            await _companyRepository.UpdateCompany(company);


            response.Status = SaveStatus.Success;
            return response;
        }



        public async Task<DeleteRequestDep> DeleteDepartment(DeleteRequestDep requestDep)
        {


            var department = await _companyRepository.GetAllActiveDepartments()
       .FirstOrDefaultAsync(d => d.Id == requestDep.Id);

            if (department == null)
                return new DeleteRequestDep { Id = requestDep.Id, Status = SaveStatus.NotFound };


            if (await _companyRepository.GetAllActiveCompanies()
                .AnyAsync(c => c.DepartmentId == requestDep.Id))
                return new DeleteRequestDep { Id = requestDep.Id, Status = SaveStatus.AlreadyInUse };

            if (!requestDep.IsActive)
            {
                department.IsActive = requestDep.IsActive;

                await _companyRepository.UpdateDepartment(department);
            }

            return new DeleteRequestDep { Id = requestDep.Id, Status = SaveStatus.Success };
        }


        // 2) Retrieve all companies for a specific DepartmentId
        public async Task<List<Company>> GetCompaniesByDepartment(int departmentId)
        {
            return await _companyRepository.GetAllActive()
                .Where(c => c.DepartmentId == departmentId)
                .ToListAsync();
        }

        // 3) Fetch the total count of companies grouped by DepartmentId
        public async Task<List<dynamic>> GetCompanyCountGroupedByDepartment()
        {
            return await _companyRepository.GetAllActive()
                .GroupBy(c => c.DepartmentId)
                .Select(g => (dynamic)new
                {
                    DepartmentId = g.Key,
                    CompanyCount = g.Count()
                })
                .ToListAsync();
        }



        // 4) Get the top 5 companies ordered by DepartmentName in descending order
        public async Task<List<Company>> GetTop5CompaniesByDepartment()
        {
            return await _companyRepository.GetAllActive()
                .OrderByDescending(c => c.Department.DepartmentName)
                .Take(5)
                .ToListAsync();
        }
    }
}

        
