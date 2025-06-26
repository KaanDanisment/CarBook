using CarBook.Application.Common.Helpers;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Commands.AppUserCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.AppUserHandlers
{
    public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommand, IResult>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppRoleRepository _appRoleRepository;

        public RegisterAppUserCommandHandler(IAppUserRepository appUserRepository, IAppRoleRepository appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<IResult> Handle(RegisterAppUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existingUser = await _appUserRepository.GetAsync(cancellationToken, user => user.Email == request.Email);
                if(existingUser != null)
                {
                    return new ErrorResult("Email already exists", "BadRequest");
                }

                if (request.AppRoleId == Guid.Empty)
                {
                    request.AppRoleId = Guid.Parse("5996ab15-10a3-4184-8716-4f08524fcf74");
                }

                var role = await _appRoleRepository.GetAsync(cancellationToken, role => role.AppRoleId == request.AppRoleId);
                if (role is null)
                {
                    return new ErrorResult("Role not found", "BadRequest");
                }
                
                var (hash, salt) = PasswordHasher.HashPassword(request.Password);
                AppUser appUser = new AppUser
                {
                    Email = request.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    AppRoleId = request.AppRoleId
                };
                await _appUserRepository.CreateAsync(cancellationToken, appUser);
                return new SuccessResult("User registered successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
