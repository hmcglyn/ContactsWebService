using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Models.Dtos
{
    public class PublicAddressDto
    {        
        public int Id { get; set; }
        public string Type { get; set; }        
        public string CityName { get; set; }        
        public string CountryName { get; set; }        
        public string ZipCode { get; set; }        
        public string Street { get; set; }       
        public string District { get; set; }
    }
}
