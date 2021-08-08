using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Description { get; set; }


        public virtual IList<User> Users { get; set; } = new List<User>();
    }
}
