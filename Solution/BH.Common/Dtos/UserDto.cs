using System;
using System.Collections.Generic;
using System.Text;

namespace BH.Common.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ProfileDto Profile { get; set; }

        public IList<RoleDto> Roles { get; set; }
    }
}
