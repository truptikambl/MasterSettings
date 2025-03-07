using System.ComponentModel.DataAnnotations;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Core.DTOs
{
    public class DyeMethodSaveRequest
    {


        public int Id { get; set; }

        [MaxLength(50)]
        public string DyeingMethods { get; set; }

        public String DyeMethodCode { get; set; }
    }

    public class DyeMethodsaveResponse : CommonEnumResponse
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    }

    public class DyeMethodPaginatedRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

    public class DyeMethodPaginatedResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }

        public List<DyeMethodSaveRequest> Data { get; set; } = new List<DyeMethodSaveRequest>();


    }

    public class DyeMethodDeleteRequest : CommonEnumResponse
    {
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

    }

}

