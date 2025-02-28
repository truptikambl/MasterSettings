using WebAPIProject.Core.Models;

namespace WebAPIProject.Core.DTOs
{
    public class DyeStuffRequest
    {

        public int Id { get; set; }
        public string DyeStuffName { get; set; }
        public bool IsActive { get; internal set; }
     
    }
    public class DyeStuffPaginatedResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<DyeStuffRequest> Data { get; set; } = new List<DyeStuffRequest>();

    }


    public class DyeStuffPaginatedRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }


    public class DyeStuffSaveResponse : CommonEnumResponse
    {
        public int Id { get; set; }


    }

    public class DyeStuffDeleteRequest : CommonEnumResponse
    {
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;



    }

}
