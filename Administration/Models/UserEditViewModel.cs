using DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class UserEditViewModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }
        public UserEditViewModel() { }
        public UserEditViewModel(User user)
        {
            UserID = user.ID;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            Email = user.Email;
            Username = user.Username;
            Active = user.Active;
        }
    }
}
