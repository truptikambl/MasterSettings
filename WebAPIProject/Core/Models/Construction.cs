using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Core.Models
{

    [Table("Construction")]
    public class Construction
    {
            public int ConstructionId { get; set; }
            public string ConstructionType { get; set; }

            public int FabricId { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("FabricId")]
        [InverseProperty("Constructions")]
        public virtual Fabricc Fabricc { get; set; }




    }
}
