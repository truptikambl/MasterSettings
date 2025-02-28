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
}
