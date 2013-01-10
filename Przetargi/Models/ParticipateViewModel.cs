using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przetargi.DataAccess.Models.Tenders;
using System.ComponentModel.DataAnnotations;

namespace Przetargi.Models
{
    public class ParticipateViewModel
    {
        public Tender Tender { get; set; }

        public int AttendeeId { get; set; }

        [Required]
        [Range(0.0, 100000000.0)]
        [Display(Name = "Cena Twojej oferty")]
        public decimal Price { get; set; }

        public ParticipateResult Result { get; set; }
    }
}