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
    public class ContactManager : IContactService
    {
        private Repository<Contact> repo_contact = new Repository<Contact>();
        private Repository<EmailAddresses> repo_emails = new Repository<EmailAddresses>();
        private Repository<PublicAddresses> repo_addresses = new Repository<PublicAddresses>();
        private Repository<PhoneNumbers> repo_phones = new Repository<PhoneNumbers>();
        private readonly IMapper _mapper;
        private readonly ILogger<ContactManager> _logger;
        public ContactManager(ILogger<ContactManager> logger,IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
        }
        public int Create(ContactDto entity)
        {
            try
            {
                var contact = _mapper.Map<Contact>(entity);
                repo_contact.Create(contact);
                return contact.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kişi ekleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }          
        }
        public List<Contact> GetContacts()
        {
            try
            {
                return repo_contact.List(g => g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Listeleme sırasında bir hata ile karşılaşıldı.");
                throw ex;
            }
        }
        public ApiResponse Delete(int contactId)
        {
            try
            {
                var contact = repo_contact.Find(g => g.Id == contactId && g.Deleted == null);
                if (contact!=null)
                {
                    repo_contact.Delete(contact);
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.OK, Status = true };
                }
                else
                {
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.NotFound, Status = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kişi silme işlemi sırasında bir hata ile karşılaşıldı.", contactId);
                throw ex;
            }
        }
        public Contact GetById(int contactId)
        {
            try
            {
                return repo_contact.Find(g => g.Id == contactId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kişiye ait herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }
        public ApiResponse Update(ContactDto entity)
        {
            try
            {
                var contact = repo_contact.Find(g => g.Id == entity.Id && g.Deleted == null);
                if (contact!=null)
                {
                    contact.Name = entity.Name;
                    contact.Surname = entity.Surname;
                    contact.Company = entity.Company;
                    repo_contact.Update(contact);
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
                _logger.LogError(ex, "Kişi güncelleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex; 
            }
        }
        public ContactDto GetByIdWithInformation(int contactId)
        {
            try
            {
                ContactDto ret = new ContactDto();
                var contact= repo_contact.Find(g => g.Id == contactId && g.Deleted == null);
                if (contact!=null)
                {
                    ret.Company = contact.Company;
                    ret.Name = contact.Name;
                    ret.Surname = contact.Surname;
                    ret.Emails = _mapper.Map<List<EmailDto>>(repo_emails.List(g => g.UserId == contactId && g.Deleted == null));
                    ret.Addresses= _mapper.Map<List<PublicAddressDto>>(repo_addresses.List(g => g.UserId == contactId && g.Deleted == null));
                    ret.PhoneNumbers= _mapper.Map<List<PhoneNumberDto>>(repo_phones.List(g => g.UserId == contactId && g.Deleted == null));
                }
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kişiye ait herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }
    }
}
