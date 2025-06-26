using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AppUser appUser);
    }
}
