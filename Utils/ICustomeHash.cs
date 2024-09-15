namespace BackendService.Utils
{
    public interface ICustomeHash
    {
        string Hash(string password);
        bool Compare(string password, string hashedPassword);
    }
}
