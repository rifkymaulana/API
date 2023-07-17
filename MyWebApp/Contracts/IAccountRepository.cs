using API.DTOs.Accounts;
using API.Utilities;

namespace MyWebApp.Contracts;

public interface IAccountRepository
{
    public Task<ResponseHandler<string>> Login(LoginAccountDto entity);
    public Task<ResponseHandler<RegisterAccountDto>> Register(RegisterAccountDto entity);
}