

using WebAPIProject.Core.Models;

namespace WebAPIProject.Core.DTOs
{


    public class SymbolCategoryRequest
    {
        public int SymbolCategoryId { get; set; }
        public string SymbolCategoryName { get; set; } = string.Empty;

    }

    public class SymbolCategoryPaginatedResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<SymbolCategoryRequest> Data { get; set; } = new List<SymbolCategoryRequest>();
      
    }

    public class SymbolCategoryPaginatedRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }


    public class SymbolCategorySaveResponse : CommonEnumResponse
    {
        public int SymbolCategoryId { get; set; }

    
    }

    public class SymbolCategoryDeleteRequest : CommonEnumResponse
    {
        public int SymbolCategoryId { get; set; }

        public bool IsActive { get; set; } = true;



    }



    public class IndividualCareSymbolRequest
    {

        public int SymbolCode { get; set; }
        public string name { get; set; }

        public string ImagePathURL { get; set; }
        public string uniqueId { get; set; }
        public string imageName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int SymbolCategoryId { get; set; }
        public int CountryId { get; set; }
    }


    public class IndividualCareSymbolPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }


    public class IndividualCareSymbolPaginatedResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<IndividualCareSymbolRequest> Data { get; set; } = new List<IndividualCareSymbolRequest>();

        public string? Search { get; internal set; }
        public int Index { get; set; }
    }

    public class IndividualCareSymbolSaveResponse : CommonEnumResponse
    {

        public int SymbolCode { get; set; }



    }

    public class IndividualCareSymbolDeleteRequest : CommonEnumResponse
    {
        public int SymbolCode { get; set; }
        public bool IsActive { get; set; } = true;

       

    }

    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }


}
