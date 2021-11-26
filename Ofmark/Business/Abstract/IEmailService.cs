using Ofmark.Entities;
using Ofmark.Models;
using Ofmark.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.Business.Abstract
{
    public interface IEmailService
    {
        int Create(int id,EmailDto entity);
        ApiResponse Update(EmailDto entity);
        ApiResponse Delete(int emailId);
        EmailAddresses GetById(int emailId);
        List<EmailAddresses> GetContactEmailAddresses(int contactId);
    }
}
