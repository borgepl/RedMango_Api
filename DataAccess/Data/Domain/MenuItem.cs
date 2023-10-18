using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Domain
{
    public class MenuItem : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SpecialTag { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }

        [Range(1, int.MaxValue)]
        public double Price { get; set; }

    }
}
