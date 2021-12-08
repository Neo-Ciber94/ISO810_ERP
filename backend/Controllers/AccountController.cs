
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
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository accountRepository;
    private readonly JwtTokenBlackListCache tokenBlacklist;

    public AccountController(IAccountRepository accountRepository, JwtTokenBlackListCache tokenBlacklist)
    {
        this.accountRepository = accountRepository;
        this.tokenBlacklist = tokenBlacklist;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> Signup(AccountSignup accountSignup)
    {
        var result = await accountRepository.Signup(accountSignup);

        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> Login(AccountLogin accountLogin)
    {
        var result = await accountRepository.Login(accountLogin);

        if (result.Success)
        {
            var account = (result as ApiResponse<AccountDto>)!.Data;
            HttpContext.SetCookieUserAccount(account);
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse>> Logout()
    {
        var dto = HttpContext.GetCookieUserAccount();

        if (dto != null)
        {
            var account = await accountRepository.GetAccountById(dto.Id);
            if (account != null)
            {
                var result = await accountRepository.Logout(account);
                HttpContext.RemoveCookieUserAccount();
                return Ok(result);
            }
        }

        return NotFound();
    }

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