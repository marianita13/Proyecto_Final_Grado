using API.Controllers;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Contoller
{
    public class UserController: BaseApiController
    {
        private readonly GardeningContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register(UserRegisterDto request)
        {
            try
            {
                var existingUser = await _unitOfWork.Users.GetUserByEmailAsync(request.email);

                if (existingUser != null)
                {
                    BadRequest("El usuario ya está registrado");
                }

                var newUser = new User
                {
                    Email = request.email,
                    Password = request.password
                };

                _unitOfWork.Users.Add(newUser);
                await _unitOfWork.SaveAsync();

                return Ok(); // Devuelve true si el usuario se registra correctamente
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, false); // Envía falso en caso de error
            }
        }
        
        [HttpPost("Login")]
        public async Task<bool> Login(UserLoginDto request)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByEmailAsync(request.email);

                if (user == null)
                {
                    return false;
                }

                bool passwordMatches= request.password == user.Password;

                return passwordMatches;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        }

}