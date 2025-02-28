using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebAPIProject.Core.Models
{
    [Table("Fabric")]
    public class Fabricc
    {
        [Key]
        public int FabricId { get; set; }
        public string FabricType { get; set; }

        public bool IsActive { get; set; } = true;

        [InverseProperty("Fabricc")]
        public virtual ICollection<Construction> Constructions { get;set;}
    }
    
}
