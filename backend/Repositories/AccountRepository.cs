using System.Threading.Tasks;
using AutoMapper;
using ISO810_ERP.Config;
using ISO810_ERP.Dtos;
using ISO810_ERP.Exceptions;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Services;
using ISO810_ERP.Services.Interfaces;
using ISO810_ERP.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ISO810_ERP.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ErpDbContext context;
    private readonly IPasswordHasher passwordHasher;
    private readonly IMapper mapper;
    private readonly TypedCache cache;

    public AccountRepository(ErpDbContext context,
        IPasswordHasher passwordHasher,
        IMapper mapper,
        TypedCache cache)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.mapper = mapper;
        this.cache = cache;
    }

    public async Task<ApiResponse> Signup(AccountSignup accountSignup)
    {
        var duplicatedUserEmail = await GetAccountByEmail(accountSignup.Email);

        if (duplicatedUserEmail != null)
        {
            return ApiResponse.Failure("Email already exists");
        }

        if (RegexUtils.IsValidEmail(accountSignup.Email) == false)
        {
            return ApiResponse.Failure("Invalid email format");
        }

        if (string.IsNullOrWhiteSpace(accountSignup.Password))
        {
            return ApiResponse.Failure("Password is required and cannot be empty");
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

    public Task<ApiResponse> Logout(string token)
    {
        cache.Set(Constants.BlackListedTokenTag, token, token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = AppSettings.JwtDuration
        });

        return Task.FromResult(ApiResponse.Successful());
    }

    public async Task<ApiResponse> Update(int id, AccountUpdate accountUpdate)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account == null)
        {
            throw new AppException("Account not found");
        }

        if (accountUpdate.Email != null && RegexUtils.IsValidEmail(accountUpdate.Email) == false)
        {
            return ApiResponse.Failure("Invalid email format");
        }

        // Change the password if the update contains a new password
        if (accountUpdate.Password != null)
        {
            if (string.IsNullOrWhiteSpace(accountUpdate.Password))
            {
                return ApiResponse.Failure("Password is required and cannot be empty");
            }

            account.PasswordHash = passwordHasher.HashPassword(accountUpdate.Password);
        }

        ObjectUtils.UpdateNonNullProperties(accountUpdate, account);
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