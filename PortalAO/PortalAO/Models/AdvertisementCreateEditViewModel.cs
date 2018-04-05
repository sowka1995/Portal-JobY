using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalAO.Models
{
    public class AdvertisementCreateEditViewModel
    {
        /// <summary>
        /// Opisuje ID ogłoszenia
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Opisuje tytuł ogłoszenia
        /// </summary>
        [Display(Name = "Tytuł ogłoszenia")]
        public string Title { get; set; }

        /// <summary>
        /// Opisuje szczegóły ogłoszenia/zlecenia
        /// </summary>
        [Display(Name = "Szczegóły ogłoszenia")]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        /// <summary>
        /// Opisuje przybliżony czas wykonania zlecenia
        /// </summary>
        [Display(Name = "Data wykonania")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ExecutionDate { get; set; }

        /// <summary>
        /// Opisuje miejsce wykonania zlecenia
        /// </summary>
        [Display(Name = "Lokalizacja")]
        public string Location { get; set; }

        /// <summary>
        /// Opisuje wielkość wynagrodzenia za zrealizowanie zlecenia
        /// </summary>
        [Display(Name = "Wielkość wynagrodzenia")]
        public decimal Payment { get; set; }
    }
}