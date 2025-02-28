using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Core.Models
{

    public class SymbolCategory
    {
        public int SymbolCategoryId { get; set; }

        public string SymbolCategoryName { get; set; }
        public bool IsActive { get; set; } = true;

        [InverseProperty("SymbolCategory")]
        public virtual ICollection<IndividualCareSymbol> IndividualCareSymbol { get; set; }
    }
}
