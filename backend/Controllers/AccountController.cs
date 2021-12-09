
using System;
using System.Threading.Tasks;
using ISO810_ERP.Dtos;
using ISO810_ERP.Extensions;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Services;
using ISO810_ERP.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISO810_ERP.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository accountRepository;
    public AccountController(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    /// <summary>
    /// Creates a new account.
    /// </summary>
    /// <param name="accountSignup">The new account credentials</param>
    /// <returns>An api response successful if the account was created, otherwise a failure api response.</returns>
    [AllowAnonymous]
    [HttpPost("signup")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> Signup(AccountSignup accountSignup)
    {
        var result = await accountRepository.Signup(accountSignup);
        return Ok(result);
    }

    /// <summary>
    /// Login using the account credentials.
    /// </summary>
    /// <param name="accountLogin">Credentials to login</param>
    /// <returns>The logged account and set jwt cookies for auth.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AccountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> Login(AccountLogin accountLogin)
    {
        var result = await accountRepository.Login(accountLogin);

        if (result.Success)
        {
            var account = (result as ApiResponse<AccountDto>)!.Data;
            HttpContext.SetCookieUserAccount(account);

        }

        return Ok(result);
    }

    /// <summary>
    /// Logout the current logged account.
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse>> Logout()
    {
        var dto = HttpContext.GetCookieUserAccount();

        if (dto != null)
        {
            var account = await accountRepository.GetAccountById(dto.Id);
            var token = HttpContext.GetJwtToken();

            if (account != null && token != null)
            {
                var result = await accountRepository.Logout(token);
                HttpContext.RemoveCookieUserAccount();
                return Ok(result);
            }
        }

        return NotFound();
    }

    /// <summary>
    /// Returns information about the current account.
    /// </summary>
    /// <returns>Information about this account.</returns>
    [HttpGet("me")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDto>> GetCurrentUser()
    {
        var dto = HttpContext.GetCookieUserAccount();

        if (dto != null)
        {
            var result = await accountRepository.GetAccountById(dto.Id);
            return Ok(result);
        }

        return NotFound();
    }

    /// <summary>
    /// Updates this account information.
    /// </summary>
    /// <param name="accountUpdate">The information to update.</param>
    /// <returns>A successful api response if the account was updated, otherwise a failure.</returns>
    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse>> UpdateAccount(AccountUpdate accountUpdate)
    {
        var dto = HttpContext.GetCookieUserAccount();

        if (dto == null)
        {
            return NotFound();
        }

        var result = await accountRepository.Update(dto.Id, accountUpdate);
        return Ok(result);
    }

    /// <summary>
    /// Delete the current account.
    /// </summary>
    /// <returns>A succesful api response if was deleted otherwise a failure.</returns>
    [HttpDelete("delete")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse>> DeleteAccount()
    {
        var dto = HttpContext.GetCookieUserAccount();

        if (dto == null)
        {
            return NotFound();
        }

        var result = await accountRepository.Delete(dto.Id);
        HttpContext.RemoveCookieUserAccount();
        return Ok(result);
    }
}