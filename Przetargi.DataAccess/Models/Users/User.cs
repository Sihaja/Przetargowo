using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Przetargi.DataAccess.Models.Users
{
    [Serializable]
    public abstract class User
    {
        public int Id { get; set; }

        public abstract UserType Type { get; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} musi składać się {2}-{1} znaków.", MinimumLength = 3)]
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        public UserStatus Status { get; set; }
    }
}
