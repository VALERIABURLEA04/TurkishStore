using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.Admin
{
    public class AdminData
    {
        public int Id { get; set; }  // Corresponds to the `Id` column
        public string Username { get; set; }  // Corresponds to the `Username` column
        public string PasswordHash { get; set; }  // Corresponds to the `PasswordHash` column
    }

}