using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ofmark.Business.Abstract;
using Ofmark.Entities;
using Ofmark.Models.Dtos;

namespace Ofmark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IAddressService _addressService;
        private IEmailService _emailService;
        private IPhoneNumberService _phoneService;
        private IContactService _contactService;
        private IMapper _mapper;
        public ContactsController(IContactService contactService,IEmailService emailService,IPhoneNumberService phoneService,
            IAddressService addressService,IMapper mapper)
        {
            _addressService = addressService;
            _phoneService = phoneService;
            _emailService = emailService;
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var contactList = _contactService.GetContacts();
                if (contactList.Count==0)
                    return NotFound();

                return Ok(_mapper.Map<List<ContactListDto>>(contactList));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var contact = _contactService.GetById(id);
                if (contact==null)
                    return NotFound();

                return Ok(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetWithInformation/{id}")]
        public IActionResult GetWithContactInformation(int id)
        {
            try
            {
                var contact = _contactService.GetByIdWithInformation(id);
                if (contact == null)
                    return NotFound();

                return Ok(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("GetContactEmailList/{id}")]
        public IActionResult GetContactEmails(int id)
        {
            try
            {
                var emails = _emailService.GetContactEmailAddresses(id);
                if (emails.Count==0)
                    return NotFound();

                return Ok(_mapper.Map<List<EmailDto>>(emails));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("GetEmail/{id}")]
        public IActionResult GetEmail(int id)
        {
            try
            {
                var email = _emailService.GetById(id);
                if (email == null)
                    return NotFound();

                return Ok(_mapper.Map<EmailDto>(email));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("CreateEmail/{id}")]
        public IActionResult CreateEmail(int id,[FromBody]EmailDto emailDto)
        {
            try
            {
                var emailId = _emailService.Create(id, emailDto);
                if (emailId > 0)
                    return Ok(emailId);
                else
                    return BadRequest("Email oluşturma işlemi sırasında bir hata ile karşılaşıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateEmail")]
        public IActionResult UpdateEmail([FromBody] EmailDto emailDto)
        {
            try
            {
                var result = _emailService.Update(emailDto);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEmail/{id}")]
        public IActionResult DeleteEmail(int id)
        {
            try
            {
                var result = _emailService.Delete(id);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetContactNumberList/{id}")]
        public IActionResult GetContactPhoneNumbers(int id)
        {
            try
            {
                var numbers = _phoneService.GetContactNumbers(id);
                if (numbers.Count == 0)
                    return NotFound();

                return Ok(_mapper.Map<List<PhoneNumberDto>>(numbers));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("GetPhoneNumber/{id}")]
        public IActionResult GetPhoneNumber(int id)
        {
            try
            {
                var number = _phoneService.GetById(id);
                if (number == null)
                    return NotFound();

                return Ok(_mapper.Map<PhoneNumberDto>(number));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("CreatePhoneNumber/{id}")]
        public IActionResult PhoneNumber(int id, [FromBody] PhoneNumberDto phoneNumberDto)
        {
            try
            {
                var numberId = _phoneService.Create(id, phoneNumberDto);
                if (numberId > 0)
                    return Ok(numberId);
                else
                    return BadRequest("Numara oluşturma işlemi sırasında bir hata ile karşılaşıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdatePhoneNumber")]
        public IActionResult UpdatePhoneNumber([FromBody] PhoneNumberDto phoneNumberDto)
        {
            try
            {
                var result = _phoneService.Update(phoneNumberDto);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeletePhoneNumber/{id}")]
        public IActionResult DeletePhoneNumber(int id)
        {
            try
            {
                var result = _phoneService.Delete(id);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetContactAddressList/{id}")]
        public IActionResult GetContactAddresses(int id)
        {
            try
            {
                var addresses = _addressService.GetContactPublicAddresses(id);
                if (addresses.Count == 0)
                    return NotFound();

                return Ok(_mapper.Map<List<PublicAddressDto>>(addresses));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("GetAddress/{id}")]
        public IActionResult GetAddress(int id)
        {
            try
            {
                var address = _addressService.GetById(id);
                if (address == null)
                    return NotFound();

                return Ok(_mapper.Map<PublicAddressDto>(address));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("CreateAddress/{id}")]
        public IActionResult CreateAddress(int id, [FromBody] PublicAddressDto addressDto)
        {
            try
            {
                var addressId = _addressService.Create(id, addressDto);
                if (addressId > 0)
                    return Ok(addressId);
                else
                    return BadRequest("Adres oluşturma işlemi sırasında bir hata ile karşılaşıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress([FromBody] PublicAddressDto addressDto)
        {
            try
            {
                var result = _addressService.Update(addressDto);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteAddress/{id}")]
        public IActionResult DeleteAddress(int id)
        {
            try
            {
                var result = _addressService.Delete(id);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateContact")]
        public IActionResult CreateContact([FromBody]ContactDto contactDto)
        {
            try
            {
                var contactId = _contactService.Create(contactDto);
                if (contactId > 0)
                    return Ok(contactId);
                else
                    return BadRequest("Kişi oluşturma işlemi sırasında bir hata ile karşılaşıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateContact")]
        public IActionResult UpdateContact([FromBody]ContactDto contactDto)
        {
            try
            {
                var result = _contactService.Update(contactDto);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteContact/{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                var result = _contactService.Delete(id);
                if (result.Status)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
