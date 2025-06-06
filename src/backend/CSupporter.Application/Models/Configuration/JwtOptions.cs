﻿namespace CSupporter.Application.Models.Configuration;

public class JwtOptions
{
    public string JwtKey { get; set; }

    public string JwtIssuer { get; set; }

    public int JwtExpireDays { get; set; }
}
