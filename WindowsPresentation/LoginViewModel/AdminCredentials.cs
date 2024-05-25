namespace WindowsPresentation.LoginViewModel;

public class AdminCredentials
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AppSettings
{
    public AdminCredentials AdminCredentials { get; set; }
}