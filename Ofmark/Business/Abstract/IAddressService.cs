using Ofmark.Entities;
using Ofmark.Models;
using Ofmark.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Business.Abstract
{
    public interface IAddressService
    {
        int Create(int id,PublicAddressDto entity);
        ApiResponse Update(PublicAddressDto entity);
        ApiResponse Delete(int emailId);
        PublicAddresses GetById(int emailId);
        List<PublicAddresses> GetContactPublicAddresses(int contactId);
    }
}
