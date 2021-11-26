using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Models.Dtos
{
    public class ContactDto
    {        
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Surname { get; set; }        
        public string Company { get; set; }
        public List<EmailDto> Emails { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
        public List<PublicAddressDto> Addresses { get; set; }
    }
}
