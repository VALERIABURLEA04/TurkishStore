using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;
using System;

namespace lab_1_templates_sandu.Controllers
{
    public class ContactController : ApiController
    {
        private DataContext _context;

        public ContactController()
        {
            _context = new DataContext(); // Initialize the context
        }

        // GET api/contact
        // Get all contact entries
        public IEnumerable<UContactData> Get() => _context.ContactData.ToList(); // Return all contact data in the database

        // GET api/contact/5
        // Get a specific contact entry by Id
        public IHttpActionResult Get(int id)
        {
            var contact = _context.ContactData.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                return NotFound(); // Return 404 if the contact with the given id doesn't exist
            }

            return Ok(contact); // Return the found contact
        }

        // POST api/contact
        // Add new contact data to the database
        public IHttpActionResult Post([FromBody] UContactData contactData)
        {
            if (contactData == null)
            {
                return BadRequest("Contact data cannot be null."); // Handle null data scenario
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            try
            {
                _context.ContactData.Add(contactData); // Add the new contact data to the context
                _context.SaveChanges(); // Save changes to the database

                return CreatedAtRoute("DefaultApi", new { id = contactData.Id }, contactData); // Return the created contact data
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during saving
                return InternalServerError(ex); // Return a 500 error with the exception details
            }
        }


        // PUT api/contact/5
        // Update an existing contact entry
        public IHttpActionResult Put(int id, [FromBody] UContactData contactData)
        {
            var existingContact = _context.ContactData.FirstOrDefault(c => c.Id == id);

            if (existingContact == null)
            {
                return NotFound(); // Return 404 if contact doesn't exist
            }

            existingContact.Name = contactData.Name;
            existingContact.Email = contactData.Email;
            existingContact.Subject = contactData.Subject;
            existingContact.Message = contactData.Message;

            _context.SaveChanges(); // Save the updated contact data

            return Ok(existingContact); // Return the updated contact data
        }

        // DELETE api/contact/5
        // Delete a specific contact entry by Id
        public IHttpActionResult Delete(int id)
        {
            var contact = _context.ContactData.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                return NotFound(); // Return 404 if contact doesn't exist
            }

            _context.ContactData.Remove(contact); // Remove the contact
            _context.SaveChanges(); // Save changes to the database

            return Ok(); // Return 200 OK response
        }

        // Dispose of the context to avoid memory leaks
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
