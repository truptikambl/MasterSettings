using WebAPIProject.Core.Models;

namespace WebAPIProject.Core.DTOs
{
    public class FabricDetails
    {

        
        
            public int FabricId { get; set; }
            public string FabricType { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;


        

        public class FabricRequest
        {
            public int FabricId { get; set; }
            public string FabricType { get; set; } = string.Empty;
            public string ConstructionType { get;  set; }

            //public string? ConstructionName { get; set; }

            //public bool IsActive { get; set; } = true;
        }


        public class FabricPaginationRequest
        {
            public int Index { get; set; } = 1;
            public int PageSize { get; set; } = 10;
            public string Search { get; set; } = "";
        }


        public class FabricPaginatedResponse : CommonEnumResponse
        {
            public int TotalRecords { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public int PageCount { get; set; }
            public List<FabricRequest> Data { get; set; } = new List<FabricRequest>();
   
        }

        public class FabricSaveResponse : CommonEnumResponse
        {
            public int FabricId { get; set; }

         
        }
        public class FabricDeleteRequest   : CommonEnumResponse
        {
            public int FabricId { get; set; }

            public bool IsActive { get; set; }
      
        }
        public class DeleteRequestCon : CommonEnumResponse
        {
            public int Id { get; set; }
            public bool IsActive { get; set; } = true;

          

        }
    

    }
}