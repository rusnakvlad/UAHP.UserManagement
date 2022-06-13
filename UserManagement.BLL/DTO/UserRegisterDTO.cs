namespace UserManagement.BLL.DTO;

public class UserRegisterDTO
{
    public string Name { get; set; }

    public string UserName { get; set; }

    public string Surname { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordConfirmation { get; set; }
}
