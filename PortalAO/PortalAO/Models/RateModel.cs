using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAO.Models
{
    /// <summary>
    /// Model oceny użytkownika
    /// </summary>
    public class RateModel : BaseModel
    {
        /// <summary>
        /// Opisuje osobę wystawiającą ocenę
        /// </summary>
        public ApplicationUser CommentingUser { get; set; }

        /// <summary>
        /// Opisuje datę oceny/komentarza
        /// </summary>
        public DateTime RateDate { get; set; }

        /// <summary>
        /// Opisuje procentowe zadowolenie z użytkownika
        /// </summary>
        public int SatisfactionPercentage { get; set; }

        /// <summary>
        /// Opisuje dodatkowe informacje uzupełniające ocenę użytkownika
        /// </summary>
        public string AdditionalComment { get; set; }
    }
}