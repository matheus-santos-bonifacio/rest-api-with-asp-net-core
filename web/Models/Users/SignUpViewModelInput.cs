using System.ComponentModel.DataAnnotations;

namespace web.Models.Users;

public class SignUpViewModelInput
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
