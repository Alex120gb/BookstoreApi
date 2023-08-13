using AutoMapper;
using BookstoreApi.Common;
using BookstoreApi.Models;
using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.TableDbContext;
using BookstoreApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepositories(UserDbContext context,
                            IMapper mapper,
                            IAuthService authService,
                            IJwtTokenGeneratorService jwtTokenGeneratorService,
                            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<int>> RegisterUser(RegisterUserModel request)
        {
            if (request == null)
            {
                return new Response<int>() { IsSuccessful = false, Value = 0 };
            }

            if (_context.UsernameExists(request.Username))
            {
                return new Response<int>() { IsSuccessful = false, Value = 0, Message = "User with same username already exists" };
            }

            request.Password = PasswordHasher.HashPassword(request.Password);
            var mapedUser = _mapper.Map<User>(request);

            _context.Users.Add(mapedUser);

            var result = await _context.SaveChangesAsync();

            return new Response<int>() { IsSuccessful = true, Value = result, Message = "User successfully registered" };
        }

        public async Task<Response<string>> Login(LoginRequestModel request)
        {
            if (request == null)
            {
                return new Response<string>() { IsSuccessful = false, Value = null };
            }

            var isAuthenticated = await _authService.AuthenticateUser(request.Username, request.Password);
            if (isAuthenticated)
            {
                var token = _jwtTokenGeneratorService.GenerateJwtTokentest(request.Username);
                _httpContextAccessor.HttpContext.Response.Headers.Add("Authorization", "Bearer " + token);
                return new Response<string>() { IsSuccessful = true, Value = token, Message = "User successfully logged in" };
            }

            return new Response<string>() { IsSuccessful = false, Value = null, Message = "Invalid credentials. Please try again." };
        }
    }
}