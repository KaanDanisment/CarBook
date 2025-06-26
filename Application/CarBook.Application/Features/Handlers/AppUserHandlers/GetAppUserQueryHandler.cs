using CarBook.Application.Common.Helpers;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Common.Security;
using CarBook.Application.Features.Queries.AppUserQueries;
using CarBook.Application.Features.Results.AppUserResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarBook.Application.Features.Handlers.AppUserHandlers
{
    public class GetAppUserQueryHandler : IRequestHandler<GetAppUserQuery, IDataResult<GetAppUserQueryResult>>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ITokenHelper _tokenHelper;

        public GetAppUserQueryHandler(IAppUserRepository appUserRepository, ITokenHelper tokenHelper)
        {
            _appUserRepository = appUserRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<GetAppUserQueryResult>> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                AppUser user = await _appUserRepository.GetAsync(cancellationToken,
                    user => user.Email == request.Email,
                    include: user => user.Include(user => user.AppRole));
                if (user is null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new ErrorDataResult<GetAppUserQueryResult>("Wrong email or password", "Unauthorize");
                }

                AccessToken token = _tokenHelper.CreateToken(user);

                GetAppUserQueryResult result = new GetAppUserQueryResult
                {
                    AccessToken = token,
                };

                return new SuccessDataResult<GetAppUserQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAppUserQueryResult>("An unexpected error occurred.", "SystemError");
            }
        }
    }
}
