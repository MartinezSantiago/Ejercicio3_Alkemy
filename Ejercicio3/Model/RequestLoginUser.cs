namespace Ejercicio3.Models
{
    public class RequestLoginUser
    {
        public RequestLoginUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
