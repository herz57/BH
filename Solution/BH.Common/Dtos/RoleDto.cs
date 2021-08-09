using System;
using System.Collections.Generic;
using System.Text;

namespace BH.Common.Dtos
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        public string Description { get; set; }

        public IList<UserDto> Users { get; set; }
    }
}
