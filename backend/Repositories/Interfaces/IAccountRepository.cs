
using System.Threading.Tasks;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;
using ISO810_ERP.Utils;

namespace ISO810_ERP.Repositories.Interfaces;

public interface IAccountRepository
{
    public Task<ApiResponse> Signup(AccountSignup accountSignup);
    public Task<ApiResponse> Login(AccountLogin accountLogin);
    public Task<ApiResponse> Logout(string token);
    public Task<ApiResponse> Update(int id, AccountUpdate account);
    public Task<ApiResponse> Delete(int id);
    public Task<AccountDto?> GetAccountById(int id);
    public Task<AccountDto?> GetAccountByEmail(string email);
}