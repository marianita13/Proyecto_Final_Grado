using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Helpers;
using API.Dtos;
using API.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Dominio.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;
public class UserService : IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Person> _passwordHasher;
    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Person> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var Person = new Person
        {
            Email = registerDto.Email,
            FirstName = registerDto.Username
        };

        Person.FirstName = _passwordHasher.HashPassword(Person, registerDto.Password); //Encrypt password

        var existingPerson = _unitOfWork.Persons
                                    .Find(u => u.FirstName.ToLower() == registerDto.Username.ToLower())
                                    .FirstOrDefault();

        if (existingPerson == null)
        {
            var rolDefault = _unitOfWork.PTypes
                                    .Find(u => u.TypeName == Helpers.Authorization.rol_default.ToString())
                                    .First();
            try
            {
                Person.PersonType = rolDefault;
                _unitOfWork.Persons.Add(Person);
                await _unitOfWork.SaveAsync();

                return $"Person  {registerDto.Username} has been registered successfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"Person {registerDto.Username} already registered.";
        }
    }
    public async Task<DataPersonDto> GetTokenAsync(LoginDto model)
    {
        DataPersonDto dataPersonDto = new DataPersonDto();
        var Person = await _unitOfWork.Persons
                    .GetByFirstNameAsync(model.Username);

        if (Person == null)
        {
            dataPersonDto.IsAuthenticated = false;
            dataPersonDto.Message = $"Person does not exist with FirstName {model.Username}.";
            return dataPersonDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(Person, Person.FirstName, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            dataPersonDto.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(Person);
            dataPersonDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            dataPersonDto.Email = Person.Email;
            dataPersonDto.FirstName = Person.FirstName;
            dataPersonDto.TypePerson = Person.PersonType.TypeName.ToList();

            if (Person.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = Person.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                dataPersonDto.RefreshToken = activeRefreshToken.Token;
                dataPersonDto.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                dataPersonDto.RefreshToken = refreshToken.Token;
                dataPersonDto.RefreshTokenExpiration = refreshToken.Expires;
                Person.RefreshTokens.Add(refreshToken);
                _unitOfWork.Persons.Update(Person);
                await _unitOfWork.SaveAsync();
            }

            return dataPersonDto;
        }
        dataPersonDto.IsAuthenticated = false;
        dataPersonDto.Message = $"Credenciales incorrectas para el usuario {Person.FirstName}.";
        return dataPersonDto;
    }
    public async Task<string> AddRoleAsync(AddRoleDto model)
    {
        var Person = await _unitOfWork.Persons
                    .GetByFirstNameAsync(model.Username);
        if (Person == null)
        {
            return $"Person {model.Username} does not exists.";
        }

        var result = _passwordHasher.VerifyHashedPassword(Person, Person.FirstName, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            var rolExists = _unitOfWork.PTypes
                                        .Find(u => u.TypeName.ToLower() == model.Role.ToLower())
                                        .FirstOrDefault();

            if (rolExists != null)
            {
                var PersonHasRole = Person.PersonType.Id.Equals(rolExists.Id);

                if (PersonHasRole == false)
                {
                    Person.PersonType = rolExists;
                    _unitOfWork.Persons.Update(Person);
                    await _unitOfWork.SaveAsync();
                }

                return $"Role {model.Role} added to Person {model.Username} successfully.";
            }

            return $"Role {model.Role} was not found.";
        }
        return $"Invalid Credentials";
    }
    public async Task<DataPersonDto> RefreshTokenAsync(string refreshToken)
    {
        var dataPersonDto = new DataPersonDto();

        var usuario = await _unitOfWork.Persons
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            dataPersonDto.IsAuthenticated = false;
            dataPersonDto.Message = $"Token is not assigned to any Person.";
            return dataPersonDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            dataPersonDto.IsAuthenticated = false;
            dataPersonDto.Message = $"Token is not active.";
            return dataPersonDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Persons.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        dataPersonDto.IsAuthenticated = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        dataPersonDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        dataPersonDto.Email = usuario.Email;
        dataPersonDto.FirstName = usuario.FirstName;
        dataPersonDto.TypePerson = usuario.PersonType.TypeName.ToList();
        dataPersonDto.RefreshToken = newRefreshToken.Token;
        dataPersonDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return dataPersonDto;
    }
    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }
    private JwtSecurityToken CreateJwtToken(Person usuario)
    {
        var roles = usuario.PersonType.People;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.FirstName));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.FirstName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}
