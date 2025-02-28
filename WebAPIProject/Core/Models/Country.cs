using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Core.Models
{
    public class Country
    {
        [Key]
        public int CounrtyId { get; set; }

        public string CountryName { get; set; }

        [InverseProperty("Countries")]
        public virtual ICollection<IndividualCareSymbol> IndividualCareSymbol { get; set; }
    }
}
