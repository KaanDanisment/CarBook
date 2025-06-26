using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.AuthenticationDtos
{
    public class JwtResponseDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
