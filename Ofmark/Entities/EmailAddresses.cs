using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Entities
{
    public class EmailAddresses : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required, StringLength(70), EmailAddress]
        public string Email { get; set; }
    }
}
