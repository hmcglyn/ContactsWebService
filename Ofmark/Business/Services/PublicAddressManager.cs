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
    public class PublicAddressManager : IAddressService
    {
        private Repository<PublicAddresses> repo_address = new Repository<PublicAddresses>();
        private Repository<Contact> repo_contact = new Repository<Contact>();
        private  readonly IMapper _mapper;
        private readonly ILogger _logger;
        public PublicAddressManager(ILogger<PublicAddressManager> logger,IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public int Create(int id,PublicAddressDto entity)
        {
            try
            {
                var user = repo_contact.Find(g => g.Id == id && g.Deleted == null);
                if (user == null)
                    return 0;
                if (entity.Type != AddressType.HOME.ToString() && entity.Type != AddressType.OTHER.ToString()
                       && entity.Type != AddressType.WORK.ToString())
                    return 0;
                var address = _mapper.Map<PublicAddresses>(entity);
                address.UserId = id;
                repo_address.Create(address);
                return address.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres ekleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }            
        }

        public ApiResponse Delete(int addressId)
        {
            try
            {
                var address = repo_address.Find(g => g.Id == addressId && g.Deleted == null);
                if (address != null)
                {
                    repo_address.Delete(address);
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.OK, Status = true };
                }
                else
                {
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.NotFound, Status = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres silme işlemi sırasında bir hata ile karşılaşıldı.", addressId);
                throw ex;
            }
        }

        public PublicAddresses GetById(int addressId)
        {
            try
            {
                return repo_address.Find(g => g.Id == addressId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }

        public List<PublicAddresses> GetContactPublicAddresses(int contactId)
        {
            try
            {
                return repo_address.List(g => g.UserId == contactId && g.Deleted == null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Herhangi bir kayıt bulunamadı.");
                throw ex;
            }
        }

        public ApiResponse Update(PublicAddressDto entity)
        {
            try
            {
                var address = repo_address.Find(g => g.Id == entity.Id && g.Deleted == null);
                if (address != null)
                {
                    if (entity.Type!=AddressType.HOME.ToString() && entity.Type!=AddressType.OTHER.ToString()
                        && entity.Type!=AddressType.WORK.ToString())
                        return new ApiResponse() { Status = false, StatusCode = (int)HttpStatusCode.PreconditionFailed };
                    address.District = entity.District;
                    address.CityName = entity.CityName;
                    address.CountryName = entity.CountryName;
                    address.Street = entity.Street;
                    address.ZipCode = entity.ZipCode;
                    address.Type = entity.Type;                    
                    repo_address.Update(address);
                    return new ApiResponse(){ StatusCode = (int)HttpStatusCode.OK, Status = true };
                }
                else
                {
                    return new ApiResponse() { StatusCode = (int)HttpStatusCode.NotFound, Status = false };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres güncelleme işlemi sırasında bir hata ile karşılaşıldı.", entity);
                throw ex;
            }
        }
    }
}
