using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TheCakeShop.Models.Data;

namespace TheCakeShop.Models.ViewModels.Account
{
    public class UserVM
    {
        public UserVM()
        {

        }

        public UserVM(UserDTO row)
        {
            Id = row.Id;
            FirstName = row.FirstName;
            LastName = row.LastName;
            Address = row.Address;
            Number = row.Number;
            EmailAddress = row.EmailAddress;
            Username = row.Username;
            Password = row.Password;
            DateOfBirth = row.DateOfBirth;
        }

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public long Number { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}