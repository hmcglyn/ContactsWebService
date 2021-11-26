using Ofmark.Entities;
using Ofmark.Models;
using Ofmark.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Business.Abstract
{
    public interface IPhoneNumberService
    {
        int Create(int id,PhoneNumberDto entity);
        ApiResponse Update(PhoneNumberDto entity);
        ApiResponse Delete(int numberId);
        PhoneNumbers GetById(int numberId);
        List<PhoneNumbers> GetContactNumbers(int contactId);
    }
}
