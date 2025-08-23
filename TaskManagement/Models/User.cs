using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models;

public class Employee
{
    public int Id { get; set; }
    [StringLength(25, ErrorMessage ="Name must be less than or equal 25 characters.")]
    public string Name { get; set; }
    [StringLength(11, ErrorMessage = "Phone no must be 11 characters.")]
    public string PhoneNo { get; set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
}
