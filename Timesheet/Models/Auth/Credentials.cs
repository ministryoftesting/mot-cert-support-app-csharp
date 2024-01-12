namespace Timesheet.Models.Auth;

public class Credentials
{

    public string Token { get; set; } = string.Empty;
    public bool Admin { get; set; }
    public int Id { get; set; }

    public Credentials()
    {
        // Jackson deserialization
    }

    public Credentials(string token, bool admin)
    {
        this.Token = token;
        this.Admin = admin;
    }
    public override string ToString()
    {
        return $"Credentials{{token='{Token}', admin={Admin}, id={Id}}}";
    }
}

