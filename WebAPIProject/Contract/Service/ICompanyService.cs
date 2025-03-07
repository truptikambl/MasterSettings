
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.Models;


namespace WebAPIProject.Contract.Service
{

    public interface ICompanyService
    {
        Task<PaginatedResponse> GetAllCompany(PaginationRequest request);
        Task<CompanySaveResponse> SaveOrUpdate(CompanyPostRequest request);
        Task<CompanyDeleteRequest> Delete(CompanyDeleteRequest request);
        Task<DeleteRequestDep> DeleteDepartment(DeleteRequestDep requestDep);


        Task<List<Company>> GetCompaniesByDepartment(int departmentId);
        Task<List<dynamic>> GetCompanyCountGroupedByDepartment();
        Task<List<Company>> GetTop5CompaniesByDepartment();
       
        

    }
}



