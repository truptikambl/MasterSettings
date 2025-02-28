using WebAPIProject.Core.Models;

namespace WebAPIProject.Core.DTOs
{



    public class SendEmailRequest
    {
        public int Id { get; set; }


    }


    public class SendEmailResponse 
    {
        public bool IsSuccess { get;  set; }
        public string Message { get;  set; }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserAdd { get; set; }

   
        public string email { get; set; }
    }
}
