using WebAPIProject.Core.Models;

public interface ICompanyRepository
{
    IQueryable<Company> GetAllActive();
    Task<int> Count(IQueryable<Company> query);
    Task<Company> GetById(int id);
    Task<int> Add(Company company);
    Task<int> UpdateCompany(Company company);
    Task<bool> IsExistingName(string companyName);
    Task<int> UpdateDepartment(Department department);
    IQueryable<Department> GetAllActiveDepartments();
    IQueryable<Company> GetAllActiveCompanies();
    Task SaveChangesAsync();
}
