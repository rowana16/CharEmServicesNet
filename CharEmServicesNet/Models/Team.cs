using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models
{
    public class Team
    {
        public int Id { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}