    using Microsoft.EntityFrameworkCore;
    using WebAPIProject.Core.Models;
    using WebAPIProject.Infrastructure.Data;

    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }
  
    public IQueryable<Company> GetAllActive()
        {
            return _context.Companies.Where(c => c.IsActive);
        }


        public async Task<int> Count(IQueryable<Company> query)
        {
            return query.Count();
        }

        public async Task<Company> GetById(int id)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> Add(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }

        public async Task<int> UpdateCompany(Company company) 
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }


        public async Task<bool> IsExistingName(string companyName)
        {
            return await _context.Companies.AnyAsync(c => c.CompanyName.Trim() == companyName);
        }


    //department


    public async Task UpdateDepartment(Department department)
    {
        var existingDepartment = await _context.Department.FindAsync(department.Id);
        if (existingDepartment != null)
        {
            _context.Entry(existingDepartment).CurrentValues.SetValues(department);
            await _context.SaveChangesAsync();
        }
    }


    public IQueryable<Department> GetAllActiveDepartments()
        {
            return _context.Department.Where(d => d.IsActive);
        }

    

    public IQueryable<Company> GetAllActiveCompanies()
        {
            return _context.Companies.Where(c => c.IsActive);
        }

    public  Task SaveChangesAsync()
    {
        return  _context.SaveChangesAsync();
    }

    async Task<int> ICompanyRepository.UpdateDepartment(Department department)
    {
        _context.Department.Update(department);
        return await _context.SaveChangesAsync();
    }
}
    
