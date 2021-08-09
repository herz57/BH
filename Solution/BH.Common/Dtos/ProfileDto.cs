using System;
using System.Collections.Generic;
using System.Text;

namespace BH.Common.Dtos
{
    public class ProfileDto
    {
        public int ProfileId { get; set; }

        public int UserId { get; set; }

        public long Balance { get; set; }

        public UserDto User { get; set; }
    }
}
