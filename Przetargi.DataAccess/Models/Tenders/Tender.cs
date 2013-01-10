using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Przetargi.DataAccess.Models.Tenders
{
    public class Tender
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} musi składać się {2}-{1} znaków.", MinimumLength = 3)]
        [Display(Name = "Nazwa przetargu")]
        public string ProjectName { get; set; }

        [Required]
        [Range(0.0, 100000000.0)] 
        [Display(Name = "Budżet przetargu")]
        public decimal ProjectBudget { get; set; }

        public DateTime PostedDate { get; set; }

        [Required]
        [RegularExpression(@"[\d]{4}-[\d]{2}-[\d]{2}", ErrorMessage = "Niepoprawna data.")]
        [Display(Name = "Data zakończenia przetargu")]
        public DateTime EndingDate { get; set; }

        public TenderStatus Status { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} musi składać się z maksymalnie {1} znaków.")]
        [Display(Name = "Imię osoby kontaktowej")]
        public string ContactPersonFirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi składać się z maksymalnie {1} znaków.")]
        [Display(Name = "Nazwisko osoby kontaktowej")]
        public string ContactPersonLastName { get; set; }

        [Required]
        [RegularExpression(@"^(\+[\d]+)?[\s]*[0-9]{9}$", ErrorMessage = "Niepoprawny numer telefonu kontaktowego")]
        [Display(Name = "Numer telefonu osoby kontaktowej")]
        public string ContactPersonTelNo { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres Email osoby kontaktowej")]
        public string ContactPersonEmail { get; set; }

        private List<TenderOffer> _offers;
        public List<TenderOffer> Offers
        {
            get 
            {
                if (_offers == null)
                {
                    _offers = Repository.Instance.GetOffersForTender(Id);
                }
                return _offers;
            }
        }
    }
}
