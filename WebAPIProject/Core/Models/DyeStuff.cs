using Microsoft.EntityFrameworkCore.Update.Internal;

namespace WebAPIProject.Core.Models
{
    public class DyeStuff
    {

        public int Id { get; set; }
        public string DyeStuffName { get; set; }
        public bool IsActive { get; set; }


        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int  UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;




    }




}



