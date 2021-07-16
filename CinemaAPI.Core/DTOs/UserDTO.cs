using CinemaAPI.Core.Enumerations;

namespace CinemaAPI.Core.DTOs
{
    public class UserDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleType? Role { get; set; }

    }
}
