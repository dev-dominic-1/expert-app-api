﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities.User;

[Table("app_user")]
public class User
{
    public int Id { get; init; }

    [MaxLength(64)] public string Name { get; set; } = "";

    [MaxLength(32)] public string Username { get; set; } = "";

    [MaxLength(256)] public string Password { get; set; } = "";
    
    public UserPhotoUrl? PhotoUrl { get; set; }
}