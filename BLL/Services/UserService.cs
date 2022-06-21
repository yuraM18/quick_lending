using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AuthenticateRequest> GetUserDetails(int id)
        {
            var person = await _unitOfWork.People.GetAsync(id);
            return _mapper.Map<Person, AuthenticateRequest>(person);
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = _unitOfWork.People.Find(x => x.Email == authenticateRequest.Username
            && x.Password == authenticateRequest.Password).FirstOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(authenticateRequest);

            return new AuthenticateResponse(user, token);
        }

        // helper methods

        private string generateJwtToken(AuthenticateRequest authenticateRequest)
        {
            var identity = GetIdentity(authenticateRequest);

            var now = DateTime.UtcNow;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    notBefore: now,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.AddHours(7),
                    signingCredentials: signIn);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(AuthenticateRequest authenticateRequest)
        {
            var user = _unitOfWork.People.Find(x => x.Email == authenticateRequest.Username
            && x.Password == authenticateRequest.Password).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                    new Claim("id", user.Id.ToString()),
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
