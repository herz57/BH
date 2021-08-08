using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Profile
    {
        public int ProfileId { get; set; }

        public int UserId { get; set; }

        public long Balance { get; set; }


        public User User { get; set; }
    }
}
