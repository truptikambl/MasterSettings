using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.Models;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }


    [HttpPost("SaveCompany")]
    public async Task<CompanySaveResponse> SaveCompany([FromBody] CompanyPostRequest companyRequest)
    {
        return await _companyService.
            SaveOrUpdate(companyRequest);
    }

    [HttpPost("GetAllCompanies")]
    public async Task<PaginatedResponse> GetAllCompanies([FromBody] PaginationRequest request)
    {
        return await _companyService.GetAllCompany(request);
    }

    [HttpPost("delete")]
    public async Task<CompanyDeleteRequest> DeleteCompany([FromBody] CompanyDeleteRequest request)
    {
        return await _companyService.Delete(request);
    }

    [HttpPost("DeleteDepartment")]
    public async Task<DeleteRequestDep> DeleteDepartment([FromBody] DeleteRequestDep request)
    {
       
        return await _companyService.DeleteDepartment(request);
    }

    //LINQ      
                                                 

    [HttpGet("company-count/grouped-by-department")]                                                                      
    public async Task<IActionResult> GetCompanyCountGroupedByDepartment()
    {
        return Ok(await _companyService.GetCompanyCountGroupedByDepartment());
    }


    [HttpGet("top-5-companies")]
    public async Task<IActionResult> GetTop5CompaniesByDepartment()
    {
        return Ok(await _companyService.GetTop5CompaniesByDepartment());
    }

    [HttpGet("companies/by-department/{departmentId}")]
    public async Task<IActionResult> GetCompaniesByDepartment()
    {
        return Ok(await _companyService.GetCompanyCountGroupedByDepartment());
    }

}

