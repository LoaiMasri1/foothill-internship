using AirportTicket.Features.User.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AirportTicket.Features.User.Models;
public class User
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(20, ErrorMessage = "Password must be at most 20 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$"
    , ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one number")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    public UserRole Role { get; set; }


    public User(
        Guid id,
        string name,
        string password,
        string email,
        UserRole role)
    {
        Id = id;
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }

    public static User Create(
        string name,
        string password,
        string email,
        UserRole role)
    {
        var user = new User(
            Guid.NewGuid(),
            name,
            password,
            email,
            role);

        return user;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Role: {Role}";
    }

}
