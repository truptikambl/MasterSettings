using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Core.Models
{
    public class IndividualCareSymbol
    {
        [Key]
        public int SymbolCode { get; set; }
        public string name { get; set; }
        public string ImagePathURL { get; set; }
        public string uniqueId { get; set; }
        public string imageName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int SymbolCategoryId { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("SymbolCategoryId")]
        [InverseProperty("IndividualCareSymbol")]
        
        public virtual SymbolCategory SymbolCategory { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("IndividualCareSymbol")]

        public virtual Country Countries { get; set; }

    }
}
