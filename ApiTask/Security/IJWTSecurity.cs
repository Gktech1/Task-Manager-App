using ApiTask.Models;

namespace ApiTask.Security
{
    public interface IJWTSecurity
    {
        public string JWTGen(AppUser user);
    }
}
