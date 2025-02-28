namespace WebAPIProject.Core.Models;

public class Company
{
    public int Id { get; set; }
    public string CompanyName { get; set; }

    public bool IsActive { get; set; } = true;
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; } 
}

public enum SaveStatus
{
    Success = 1,  
    NotFound = 2,
    Failed = 3,
    NameAllReadyExist = 4,
    AlreadyInUse = 5,
    Error = 6
}

