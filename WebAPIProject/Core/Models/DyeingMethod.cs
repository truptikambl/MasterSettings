namespace WebAPIProject.Core.Models
{
    public class DyeingMethod
    {
        public int Id { get; set; }
        public string DyeMethods { get; set; }

        public string DyeMethodCode { get; set; }
        public bool IsActive { get; set; } = true;

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
