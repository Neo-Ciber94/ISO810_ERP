using System.Threading.Tasks;
using AutoMapper;
using ISO810_ERP.Dtos;
using ISO810_ERP.Exceptions;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Services.Interfaces;
using ISO810_ERP.Utils;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ErpDbContext context;
    private readonly IPasswordHasher passwordHasher;
    private readonly IMapper mapper;

    public AccountRepository(ErpDbContext context,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Signup(AccountSignup accountSignup)
    {
        var duplicatedUserEmail = await GetAccountByEmail(accountSignup.Email);

        if (duplicatedUserEmail != null)
        {
            return ApiResponse.Failure("Email already exists");
        }

        var account = new Account
        {
            Name = accountSignup.Name,
            Email = accountSignup.Email,
            PasswordHash = passwordHasher.HashPassword(accountSignup.Password),
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync();
        return ApiResponse.Successful();
    }

    public async Task<ApiResponse> Login(AccountLogin accountLogin)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(e => e.Email == accountLogin.Email);

        if (account == null)
        {
            return ApiResponse.Failure("Email or password is incorrect");
        }

        if (!passwordHasher.Verify(account.PasswordHash, accountLogin.Password))
        {
            return ApiResponse.Failure("Email or password is incorrect");
        }


        var accountDto = mapper.Map<AccountDto>(account);
        return ApiResponse.Json(accountDto);
    }

    public Task<ApiResponse> Logout(AccountDto account)
    {
        return Task.FromResult(ApiResponse.Successful());
    }

    public async Task<ApiResponse> Update(int id, AccountUpdate account)
    {
        var accountToUpdate = await context.Accounts.FindAsync(id);

        if (accountToUpdate == null)
        {
            throw new AppException("Account not found");
        }

        // Change the password if the update contains a new password
        if (account.Password != null)
        {
            accountToUpdate.PasswordHash = passwordHasher.HashPassword(account.Password);
        }

        accountToUpdate.Name = account.Name;
        accountToUpdate.Email = account.Email;

        await context.SaveChangesAsync();
        return ApiResponse.Successful();
    }

    public async Task<ApiResponse> Delete(int id)
    {
        var accountToDelete = await context.Accounts.FindAsync(id);

        if (accountToDelete == null)
        {
            throw new AppException("Account not found");
        }

        context.Accounts.Remove(accountToDelete);
        await context.SaveChangesAsync();
        return ApiResponse.Successful();
    }

    public async Task<AccountDto?> GetAccountByEmail(string email)
    {
        var result = await context.Accounts.FirstOrDefaultAsync(a => a.Email == email);

        if (result == null)
        {
            return null;
        }

        return mapper.Map<AccountDto>(result);
    }

    public async Task<AccountDto?> GetAccountById(int id)
    {
        var result = await context.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        if (result == null)
        {
            return null;
        }

        return mapper.Map<AccountDto>(result);
    }
}