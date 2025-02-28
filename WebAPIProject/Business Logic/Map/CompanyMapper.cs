using WebAPIProject.Core.Models;

public class CompanyMapper
{
    public Company saveentity(CompanyPostRequest request)
    {
        if (request == null)
        {
            return null;
        }

        return new Company
        {
            CompanyName = request.CompanyName?.Trim(),
            DepartmentId = request.DepartmentId,
            IsActive = request.IsActive
        };
    }

    public Company UpdatedEntity(Company entity, CompanyPostRequest request)
    {
        entity.CompanyName = request.CompanyName?.Trim();
        entity.DepartmentId = request.DepartmentId;    
        entity.IsActive = request.IsActive;
        return entity;
    }

   
        public  Department MapToEntity(DeleteRequestDep dto)
        {
            return new Department
            {
                Id = dto.Id,
                IsActive = dto.IsActive
            };
        
    }
}

