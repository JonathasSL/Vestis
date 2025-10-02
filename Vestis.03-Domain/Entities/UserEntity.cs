﻿using System.ComponentModel.DataAnnotations;

namespace Vestis._03_Domain.Entities;

public class UserEntity : BaseEntity<Guid>
{
    public string Name { get; private set; }
    [EmailAddress]
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsEmailConfirmed { get; private set; }

    public UserEntity(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        IsEmailConfirmed = false;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public UserEntity() { }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");
        else if (Name != name)
        {
            Name = name;
            SetAsUpdated();
        }
    }
    public void ChangeEmail(string email)
    {
        if (Email != email)
        {
            Email = email;
            SetAsUpdated();
        }
    }
    public void ChangePassword(string password)
    {
        if (Password != password)
        {
            Password = password;
            SetAsUpdated();
        }
    }
}
