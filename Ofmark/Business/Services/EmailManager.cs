using AutoMapper;
using Microsoft.Extensions.Logging;
using Ofmark.Business.Abstract;
using Ofmark.Entities;
using Ofmark.Models;
using Ofmark.Models.Dtos;
using Ofmark.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ofmark.Business.Services
{
    public class EmailManager : IEmailService
    {
        private Repository<EmailAddresses> repo_email = new Repository<EmailAddresses>();
        private Repository<Contact> repo_contact = new Repository<Contact>();
        private readonly IMapper _mapper;
        private readonly ILogger<EmailManager> _logger;
        public EmailManager(ILogger<EmailManager> logger,IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public int Create(int id,EmailDto entity)
        {
            try
            {
                var user = repo_contact.Find(g => g.Id == id && g.Deleted == null);
                if (user == null)
                    return 0;
                var email = _mapper.Map<EmailAddresses>(entity);
                email.UserId = id;
                repo_email.Create(email);
                return email.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email ekleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }
        }

        public ApiResponse Delete(int emailId)
        {
            try
            {
                var email = repo_email.Find(g => g.Id == emailId && g.Deleted == null);
                if (email != null)
                {
                    repo_email.Delete(email);
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.OK, Status = true };
                }
                else
                {
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.NotFound, Status = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email silme işlemi sırasında bir hata ile karşılaşıldı.", emailId);
                throw ex;
            }
        }

        public EmailAddresses GetById(int emailId)
        {
            try
            {
                return repo_email.Find(g => g.Id == emailId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }

        public List<EmailAddresses> GetContactEmailAddresses(int contactId)
        {
            try
            {
                return repo_email.List(g => g.UserId==contactId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }

        public ApiResponse Update(EmailDto entity)
        {
            try
            {
                var email = repo_email.Find(g => g.Id == entity.Id && g.Deleted == null);
                if (email != null)
                {
                    email.Email=entity.Email;                    
                    repo_email.Update(email);
                    return new ApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = true
                    };
                }
                else
                {
                    return new ApiResponse()
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email güncelleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }
        }
    }
}
