
using WebAPIProject.Core.Models;
using static WebAPIProject.Core.DTOs.FabricDetails;

public class Companydetails
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;


}
public class CompanydetailsPaginationResponse
{
    public List<Companydetails> Companydetails { get; set; }

    public SaveStatus Result { get; set; }
}


public class CompanyPostRequest
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    //  public SaveStatus Status { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; internal set; }
}
public class CompanySaveResponse
{
    public int Id { get; set; }
    public SaveStatus Status { get; set; }
}

public class PaginationRequest
{
    public int Index { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Search { get; set; } = "";
    public int DepartmentId { get; set; }
}


public class PaginatedResponse
{
    public int TotalRecords { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int PageCount { get; set; }
    public List<CompanyPostRequest> Data { get; set; } = new List<CompanyPostRequest>();



    public SaveStatus Status { get; set; }
}




public class CompanyDeleteRequest
{
    public int Id { get; set; }

    public bool IsActive { get; set; }
    public SaveStatus Status { get; set; }
}


public class DeleteRequestDep
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;

    public SaveStatus Status { get; set; }
    public CommonEnum Enum { get; internal set; }
}

public class CompanyCountByDepartmentDto
{
    public int? DepartmentId { get; set; }
    public int CompanyCount { get; set; }
}

