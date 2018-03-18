using motekarteknologi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace motekarteknologi.Areas.security.Models
{
    public class Company 
    {
        public Guid CompanyID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<CompanyUser> CompanyUsers { get; } = new List<CompanyUser>();
    }
}
