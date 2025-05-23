﻿namespace CSupporter.Domain.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Nationality { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; }
}
