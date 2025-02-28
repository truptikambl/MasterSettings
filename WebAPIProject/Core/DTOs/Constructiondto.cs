using WebAPIProject.Core.Models;
using static WebAPIProject.Core.DTOs.FabricDetails;

namespace WebAPIProject.Core.DTOs
{
    public class Constructiondto
    {
        public int ConstructionId { get; set; }

        public string ConstructionType { get; set; }
        public int FabricId { get; set; }
        public bool IsActive { get; set; } = true;






        public class ConstructionRequest
        {

            public int ConstructionId { get; set; }
            public string? ConstructionType { get; set; }

            public int FabricId { get; set; }

            public string FabricName { get; set; }

            public bool IsActive { get; set; } = true;
        }


        public class ConstructionPaginationRequest
        {
            public int Index { get; set; } = 1;
            public int PageSize { get; set; } = 10;
            public string Search { get; set; } = "";
        }


        public class ConstructionPaginatedResponse : CommonEnumResponse
        {
            public int TotalRecords { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public int PageCount { get; set; }
            public List<ConstructionRequest> Data { get; set; } = new List<ConstructionRequest>();

            public string? Search { get; internal set; }
            public int Index { get; internal set; }
        }

        public class ConstructionSaveResponse : CommonEnumResponse
        {
            public int ConstructionId { get; set; }

        }

        public class ConstructionDeleteRequest : CommonEnumResponse
        {
            public int Id { get; set; }
            public bool IsActive { get; set; } = true;

        }



    }
}
