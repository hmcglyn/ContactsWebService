using Ofmark.Entities;
using Ofmark.Models;
using Ofmark.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Business.Abstract
{
    public interface IContactService
    {
        int Create(ContactDto entity);
        ApiResponse Update(ContactDto entity);
        ApiResponse Delete(int contactId);        
        Contact GetById(int contactId);
        ContactDto GetByIdWithInformation(int contactId);
        List<Contact> GetContacts();
    }
}
