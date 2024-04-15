using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RefreshToken;
public class ApplicantRegisterCommand : IRequest<RegisteredResponse>
{
    public ApplicantRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public ApplicantRegisterCommand()
    {
        UserForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public ApplicantRegisterCommand(ApplicantRegisterDto userForRegisterDto, string ipAddress)
    {
        UserForRegisterDto = userForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<ApplicantRegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IApplicantRepository _applicantRepository;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IApplicantRepository applicantRepository)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _applicantRepository = applicantRepository;
        }

        public async Task<RegisteredResponse> Handle(ApplicantRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.UserForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Applicant newUser =
                new()
                {
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    NationalIdentity = request.UserForRegisterDto.NationalIdentity,
                    DateOfBirth = request.UserForRegisterDto.DateOfBirth,
                    About = request.UserForRegisterDto.About,
                    UserName = request.UserForRegisterDto.UserName,
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Applicant createdUser = await _applicantRepository.AddAsync(newUser);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
