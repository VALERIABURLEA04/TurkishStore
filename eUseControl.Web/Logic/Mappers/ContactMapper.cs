using eUseControl.Domain.Entities.Contact;
using eUseControl.Web.Models.Contact;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.Contact.eUseControl.Domain.Entities.Contact;  // Assuming UContactData is here

namespace eUseControl.Web.Logic.Mappers
{
    public static class ContactMapper
    {
        public static ContactMessageModel ToModel(ContactMessageEntity entity)
        {
            if (entity == null) return null;

            return new ContactMessageModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Subject = entity.Subject,
                Message = entity.Message
            };
        }

        public static ContactMessageEntity ToEntity(ContactMessageModel model)
        {
            if (model == null) return null;

            return new ContactMessageEntity
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            };
        }

        // NEW: Map from UContactData (user input) to ContactMessageEntity
        public static ContactMessageEntity ToEntity(UContactData data)
        {
            if (data == null) return null;

            return new ContactMessageEntity
            {
                Name = data.Name,
                Email = data.Email,
                Subject = data.Subject,
                Message = data.Message
            };
        }
    }
}
