using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace motekarteknologi.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        public Guid ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
    }
}
