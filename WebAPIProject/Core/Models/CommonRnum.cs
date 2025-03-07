namespace WebAPIProject.Core.Models
{
    public class CommonEnumResponse
    {
        public CommonEnum Result { get; set; }
    }

    public enum CommonEnum
    {
        Success = 1,
        NotFound = 2,
        Failed = 3,
        NameAlReadyExist = 4,
        AlreadyInUse = 5,
        Error = 6
    }

    namespace MyWebApi.Core.Model
    {
        public class NotificationType
        {

        }

        public enum Notify
        {
            CompanyAdded = 1,
            DepartmentCreated = 2,
            DyeStuffCreated = 3,
            EmailSentSuccessfully = 4
        }
    }

}
