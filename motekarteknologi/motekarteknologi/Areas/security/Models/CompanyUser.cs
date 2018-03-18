using motekarteknologi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace motekarteknologi.Areas.security.Models
{
    public class CompanyUser
    {
        public Guid CompanyID { get; set; }
        public Company Company { get; set; }
        
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
