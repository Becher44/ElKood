#nullable disable

using System.ComponentModel.DataAnnotations;

namespace ElKood.Application.Common;

public class AppSettings
{
    public string AppUrl { get; set; }
    public ApplicationDetail ApplicationDetail { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public Identity Identity { get; set; }
    public bool UseInMemoryDatabase { get; set; }
    public string[] Cors { get; set; }
    public string BaseURL { get; set; }
}

public class ApplicationDetail
{
    public string ApplicationName { get; set; }
    public string Description { get; set; }
}

public class ConnectionStrings
{
    [Required]
    public string DefaultConnection { get; set; }
}

public class Identity
{
    [Required]
    public bool IsLocal { get; set; } = false;
    [Required]
    public string Key { get; set; }
    [Required]
    public string Issuer { get; set; }
    [Required]
    public string Audience { get; set; }
    [Required]
    public string ScopeBaseDomain { get; set; }
    [Required]
    public bool ValidateHttps { get; set; }
    public int ExpiredTime { get; set; } = 10;
}

public class RequestResponse
{
    public bool IsEnabled { get; set; } = true;
}



