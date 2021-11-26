using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Entities
{
    public class Contact : BaseEntity
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string Surname { get; set; }
        [StringLength(30)]
        public string Company { get; set; }
    }
}
