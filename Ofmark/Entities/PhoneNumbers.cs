using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Entities
{
    public class PhoneNumbers : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
