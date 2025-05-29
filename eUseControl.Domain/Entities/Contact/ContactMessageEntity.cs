using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Contact
{
    using System.ComponentModel.DataAnnotations;

    namespace eUseControl.Domain.Entities.Contact
    {
        public class ContactMessageEntity
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Name is required.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Email is not valid.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Subject is required.")]
            public string Subject { get; set; }

            [Required(ErrorMessage = "Message is required.")]
            public string Message { get; set; }
        }
    }

}
