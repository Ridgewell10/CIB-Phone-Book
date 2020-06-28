using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Infustructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
       
        private readonly ILoggerManager _logger;
        private readonly IContactRepository _repo;
        public ContactsController(ILoggerManager logger,IContactRepository contact)
        {
            _logger = logger;
           
            _repo = contact;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllContacts()
        {
            try
            {
                var contacts = await _repo.GetAllContactsAsync();

                _logger.LogInfo($"Returned all contacts from database.");

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllContactsAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ContactById")]
        public async Task <IActionResult> GetContactById(int contactId)
        {
            try
            {
                var contact = await _repo.GetContactByIdAsync(contactId);

                if (contact == null)
                {
                    _logger.LogError($"Contact with id: {contactId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned contact with id: {contactId}");
                    return Ok(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetContactByIdAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddContact([FromBody]PhoneBook contact)
        {
            try
            {
                if (contact == null)
                {
                    _logger.LogError("Contact object sent from client is null.");
                    return BadRequest("Contact object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid contact object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repo.AddContact(contact);

                return CreatedAtRoute("ContactById", new { id = contact.Id }, contact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateContact([FromBody]PhoneBook contact)

        {
            try
            {
                if (contact == null)
                {
                    _logger.LogError("Contact object sent from client is null.");
                    return BadRequest("Contact object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid contact object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbContact = await _repo.GetContactByIdAsync(contact.Id);
                if (dbContact == null)
                {
                    _logger.LogError($"Contact with id: {contact}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repo.UpdateContact(contact))
                {
                    _logger.LogError($"Contact : {contact}, hasn't been found in db.");
                    return NoContent();
                }
                else
                    _logger.LogError($"Problem while updating Contact  try later : {contact}, hasn't been found in db.");
                return BadRequest("Problem while updating try later");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteContact(PhoneBook contact, int contactId)
        {
            try
            {
                var dbContact = await _repo.GetContactByIdAsync(contactId);
                if (dbContact == null)
                {
                    _logger.LogError($"Employee with id: {contactId}, hasn't been found in db.");
                    return NotFound();
                }
                if (contactId != 0)
                {
                    _repo.DeleteContact(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return Ok();
        }
    }
}