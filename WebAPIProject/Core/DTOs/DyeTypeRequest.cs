using System.ComponentModel.DataAnnotations;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Core.DTOs
{

    public class DyeTypesaveRequest
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string DyeTypeName { get; set; }

        public string DyeTypeCode { get; set; }
    }

    public class DyeTypesaveResponse : CommonEnumResponse
    {
        public int Id { get; set; }
        //public string DyeTypeName { get; set; }
        //public string DyeTypeCode { get; set; }
        public bool IsActive { get; set; }
       /* public int CustomerId { get; set; }*/
        //public int CreatedBy { get; set; }

    }

    public class DyeTypePaginatedRequest 
    { 
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

    public class DyeTypePaginatedResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }

        public List<DyeTypesaveRequest> Data { get; set; } = new List<DyeTypesaveRequest>();
       

    }

    public class DyeTypeDeleteRequest : CommonEnumResponse 
    {
        public int Id { get; set; }
        
        public bool IsActive { get; set; } = true;

    }

}
