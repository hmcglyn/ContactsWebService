using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Models.Dtos
{
    public class PhoneNumberDto
    {        
        public int Id { get; set; }        
        public string Type { get; set; }        
        public string Number { get; set; }
    }
}
