using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Core.Models
{
 [Table("Department")] 
    public class Department
    {

        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; internal set; }

        public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
    }
}
