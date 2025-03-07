using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPIProject.Core.Models.MyWebApi.Core.Model;

namespace WebAPIProject.Core.Models
{

    public class Notification
    {
        public int Id { get; set; }
        public Notify NotificationType { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;

    }


}

