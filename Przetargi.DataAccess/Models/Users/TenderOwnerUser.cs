using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Przetargi.DataAccess.Models.Users
{
    [Serializable]
    public class TenderOwnerUser : User
    {
        public override UserType Type { get { return UserType.TenderOwner; } }

        [Required]
        [RegularExpression(@"^(\+[\d]+)?[\s]*[0-9]{9}$", ErrorMessage = "Niepoprawny numer telefonu kontaktowego")]
        [Display(Name = "Numer telefonu kontaktowego (komórkowego)")]
        public string TelephoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}-[0-9]{2}-[0-9]{2}-[0-9]{3}$", ErrorMessage = "Niepoprawny format NIP ( 000-00-00-000 ).")]
        [Display(Name = "Numer Identyfikacji Podatkowej")]
        public string NIP { get; set; }

        [Required]
        [RegularExpression(@"^[VXI]*$", ErrorMessage = "Niepoprawny format KRS - użyj liczb rzymskich.")]
        [Display(Name = "Numer KRS")]
        public string KRS { get; set; }

        [StringLength(14, ErrorMessage = "{0} musi składać się przynajmniej z dokładnie {2} cyfr.", MinimumLength = 14)]
        [Display(Name = "REGON")]
        public string REGON { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} musi składać się {2}-{1} znaków.", MinimumLength = 2)]
        [Display(Name = "Nazwa firmy")]
        public string CompanyName { get; set; }
    }
}
