﻿namespace UserManagement.BLL.DTO;

public class UserProfileDTO
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public int AdsAmount { get; set; }

    public int ComentsAmount { get; set; }
}
