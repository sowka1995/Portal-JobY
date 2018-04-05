using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalAO.Models
{
    /// <summary>
    /// Model ogłoszenia/zlecenia
    /// </summary>
    public class AdvertisementModel : BaseModel
    {
        /// <summary>
        /// Opisuje tytuł ogłoszenia
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Opisuje właściciela ogłoszenia
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Opisuje szczegóły ogłoszenia/zlecenia
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Opisuje przybliżony czas wykonania zlecenia
        /// </summary>
        public DateTime ExecutionDate { get; set; }

        /// <summary>
        /// Opisuje miejsce wykonania zlecenia
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Opisuje wielkość wynagrodzenia za zrealizowanie zlecenia
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        /// Opisuje ID zainteresowanych zleceniobiorców odzielone przecinkami
        /// </summary>
        public string InterestedContractorStorage { get; set; }

        /// <summary>
        /// Opisuje kolekację 
        /// </summary>
        [NotMapped]
        public virtual List<int> InterestedContractorIDs
        {
            get
            {
                if (InterestedContractorStorage != "")
                {
                    return InterestedContractorStorage.Split(';').Select(Int32.Parse).ToList<int>();
                }
                else
                {
                    return new List<int>();
                }
            }
            set
            {
                InterestedContractorStorage = string.Join(";", value.OrderBy(x => x).Distinct().ToArray());
            }
        }

        /// <summary>
        /// Opisuje zatwierdzonego zleceniobiorce przez zleceniodawce
        /// </summary>
        public int SelectedMandatoryID { get; set; }

        /// <summary>
        /// Opisuje czy wybrano już zleceniobiorce
        /// </summary>
        public bool IsMandatorySelected { get; set; }
        
        /// <summary>
        /// Opisuje czy zlecenie zostało wykonane i zakończone
        /// </summary>
        public bool IsExecuted { get; set; }
    }
}