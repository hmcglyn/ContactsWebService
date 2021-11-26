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
using static Ofmark.Entities.Enumerations;

namespace Ofmark.Business.Services
{
    public class PhoneNumberManager : IPhoneNumberService
    {
        private Repository<PhoneNumbers> repo_numbers = new Repository<PhoneNumbers>();
        private Repository<Contact> repo_contact = new Repository<Contact>();
        private IMapper _mapper;
        private ILogger _logger;
        public PhoneNumberManager(ILogger<PhoneNumberManager> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public int Create(int id,PhoneNumberDto entity)
        {
            try
            {
                var user = repo_contact.Find(g => g.Id == id && g.Deleted == null);
                if (user == null)
                    return 0;
                var number = _mapper.Map<PhoneNumbers>(entity);
                if (number.Type != PhoneNumberType.GSM.ToString() && number.Type != PhoneNumberType.HOME.ToString()
                    && number.Type != PhoneNumberType.OTHER.ToString() && number.Type != PhoneNumberType.WORK.ToString())
                    return 0;
                number.UserId = id;
                repo_numbers.Create(number);                
                return number.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Telefon numarası ekleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }
        }

        public ApiResponse Delete(int numberId)
        {
            try
            {
                var number = repo_numbers.Find(g => g.Id == numberId && g.Deleted == null);
                if (number != null)
                {
                    repo_numbers.Delete(number);
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.OK, Status = true };
                }
                else
                {
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.NotFound, Status = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Telefon numarası silme işlemi sırasında bir hata ile karşılaşıldı.", numberId);
                throw ex;
            }
        }

        public PhoneNumbers GetById(int numberId)
        {
            try
            {
                return repo_numbers.Find(g => g.Id == numberId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }

        public List<PhoneNumbers> GetContactNumbers(int contactId)
        {
            try
            {
                return repo_numbers.List(g => g.UserId == contactId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }

        public ApiResponse Update(PhoneNumberDto entity)
        {
            try
            {
                var number = repo_numbers.Find(g => g.Id == entity.Id && g.Deleted == null);
                if (number != null)
                {
                    if (number.Type != PhoneNumberType.GSM.ToString() && number.Type != PhoneNumberType.HOME.ToString()
                       && number.Type != PhoneNumberType.OTHER.ToString() && number.Type != PhoneNumberType.WORK.ToString())
                        return new ApiResponse() { Status=false,StatusCode=(int)HttpStatusCode.PreconditionFailed };
                    number.Type = entity.Type;
                    number.Number = entity.Number;
                    repo_numbers.Update(number);
                    return new ApiResponse(){ StatusCode = (int)HttpStatusCode.OK,Status = true};
                }
                else
                {
                    return new ApiResponse(){ StatusCode = (int)HttpStatusCode.NotFound, Status = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Telefon numarası güncelleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }
        }
    }
}
