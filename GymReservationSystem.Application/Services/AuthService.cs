using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Domain.Entities;
using GymReservationSystem.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymReservationSystem.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository,IUserRoleRepository userRoleRepository,string secretKey,string issuer,string audience)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new Exception("Invalid email or password");
        }

        var userRoles = await _userRoleRepository.GetByUserIdAsync(user.Id);
        var roleName = userRoles.FirstOrDefault()?.Role.Name ?? "User";

        var token = GenerateToken(user, roleName);
        return new AuthResponseDto 
        { 
            Token = token, 
            Email = user.Email, 
            Role = roleName 
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
        {
            throw new Exception("Email already exists");
        }

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Phone = dto.Phone,
            IsActive = true
        };
        var created = await _userRepository.CreateAsync(user);

        var userRole = new UserRole { UserId = created.Id, RoleId = 2 };
        await _userRoleRepository.CreateAsync(userRole);

        var token = GenerateToken(created, "User");
        return new AuthResponseDto 
        { 
            Token = token, 
            Email = created.Email, 
            Role = "User" 
        };
    }

    private string GenerateToken(User user, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role)
        };
        var token = new JwtSecurityToken(_issuer, _audience, claims,
            expires: DateTime.Now.AddHours(8), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}