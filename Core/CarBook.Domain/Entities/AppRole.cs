using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class AppRole
    {
        public Guid AppRoleId { get; set; }
        public string AppRoleName { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
    }
}
