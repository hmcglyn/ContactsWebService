using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Entities
{
    public class PublicAddresses : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(10)]
        public string Type { get; set; }
        [StringLength(20)]
        public string CityName { get; set; }
        [StringLength(20)]
        public string CountryName { get; set; }
        [StringLength(10)]
        public string ZipCode { get; set; }
        [StringLength(150)]
        public string Street { get; set; }
        [StringLength(150)]
        public string District { get; set; }
    }
}
