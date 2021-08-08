using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Domain
    {
        [Key]
        public DomainType DomainType { get; set; }

        public string Description { get; set; }


        public virtual IList<Machine> Machines { get; set; } = new List<Machine>();
    }
}
