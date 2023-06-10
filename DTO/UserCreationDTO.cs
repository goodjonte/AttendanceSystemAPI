using AttendanceSystemAPI.Models;

namespace AttendanceSystemAPI.DTO
{
    public class UserCreationDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UsersRole { get; set; }
        public string? ParentName { get; set; } //shouldnt be null if user role is student 2
        public string? ParentPhone { get; set; } //shouldnt be null if user role is student 2
    }
}
