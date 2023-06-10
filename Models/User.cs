namespace AttendanceSystemAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool CanLogin { get; set; }
        public string? Email { get; set; } //must be unique used to login
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public UserRole UsersRole { get; set; }
        public string? ParentName { get; set; } //shouldnt be null if user role is student / 2
        public string? ParentPhone { get; set; } //shouldnt be null if user role is student / 2
    }

    public enum UserRole
    {
        Admin = 0,
        Teacher = 1,
        Student = 2,
    }
}
