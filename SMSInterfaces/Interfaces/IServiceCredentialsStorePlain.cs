namespace SMSInterfaces.Interfaces
{
    public interface IServiceCredentialsStorePlain:ICredentialStore 
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}