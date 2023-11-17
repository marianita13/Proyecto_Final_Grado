using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;

namespace API.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto model);
    Task<DataPersonDto> GetTokenAsync(LoginDto model);
    Task<string> AddRoleAsync(AddRoleDto model);
    Task<DataPersonDto> RefreshTokenAsync(string refreshToken);
    }
}