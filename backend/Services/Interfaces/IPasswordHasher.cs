
namespace ISO810_ERP.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string hashedPassword, string password);
    }
}